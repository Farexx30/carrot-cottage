using CarrotCottage.Scripts.Characters.PlayerScripts.Inputs;
using CarrotCottage.Scripts.Components;
using CarrotCottage.Scripts.Dialogues;
using CarrotCottage.Scripts.Globals;
using Godot;
using Godot.Collections;
using System;
using System.Threading.Tasks;

namespace CarrotCottage.Scripts.Objects.AnimalHouseScripts;

public partial class BaseAnimalHouse : Node2D
{
    private readonly PackedScene _dialogueBalloonScene = GD.Load<PackedScene>("res://Scenes/Dialogues/DialogueBalloon.tscn");

    [Export]
    public StringName InteractStartCommand { get; set; } = default!;

    [Export]
    public int FoodDropHeight { get; set; } = 40;

    [Export]
    public int RewardXShift { get; set; } = 5;

    [Export]
    public PackedScene InputRewardScene { get; set; } = default!;

    [Export]
    public StringName InputRewardSceneName { get; set; } = default!;

    [Export]
    public Array<PackedScene> OutputRewardScenes { get; set; } = [];


    private CarrotCottageDialogueManager _dialogueManager = default!;
    private InventoryManager _inventoryManager = default!;

    private InteractableComponent _interactableComponent = default!; 
    private Control _interactableLabelComponent = default!; 
    private FeedComponent _feedComponent = default!; 
    private Marker2D _rewardMarker2D = default!;

    private bool _isPlayerInRange = false;
    private int _currentXShift = 0; 

    public override void _Ready()
    {
        _dialogueManager = GetNode<CarrotCottageDialogueManager>(GlobalNames.DialogueManager);
        _inventoryManager = GetNode<InventoryManager>(GlobalNames.InventoryManager);
        _interactableComponent = GetNode<InteractableComponent>(BaseAnimalHouseConstants.Nodes.InteractableComponent);
        _interactableLabelComponent = GetNode<Control>(BaseAnimalHouseConstants.Nodes.InteractableLabelComponent);
        _feedComponent = GetNode<FeedComponent>(BaseAnimalHouseConstants.Nodes.FeedComponent);
        _rewardMarker2D = GetNode<Marker2D>(BaseAnimalHouseConstants.Nodes.RewardMarker2D);

        _interactableLabelComponent.Visible = false;

        _dialogueManager.FeedTheAnimals += OnFeedTheAnimals;
        _interactableComponent.InteractableActivated += OnInteractableActivated;
        _interactableComponent.InteractableDeactivated += OnInteractableDeactivated;
        _feedComponent.FoodReceived += OnFoodReceived;
    }

    private async void OnFeedTheAnimals()
    {
        if (_isPlayerInRange)
        {
            await TriggerFeedHarvest(InputRewardSceneName, InputRewardScene);
        }
    }

    private void OnInteractableActivated()
    {
        _interactableLabelComponent.Visible = true;
        _isPlayerInRange = true;
    }

    private void OnInteractableDeactivated()
    {
        _interactableLabelComponent.Visible = false;
        _isPlayerInRange = false;

        _currentXShift = 0;
    }

    private void OnFoodReceived(Area2D food)
    {
        CallDeferred(nameof(AddRewardScene));
    }

    private async Task TriggerFeedHarvest(StringName _inventoryItem, PackedScene scene)
    {
        if (!_inventoryManager.Inventory.TryGetValue(_inventoryItem, out var count))
        {
            return;
        }

        for (int i = 0; i < count; ++i)
        {
            var harvestInstance = scene.Instantiate<Node2D>();

            // Remove the "2" collectable component collision mask to prevent the harvestable from being collected by the player during feed animation.
            var harvestCollectableComponent = harvestInstance.GetNodeOrNull<Area2D>(ComponentNames.CollectableComponent);
            harvestCollectableComponent.CollisionMask &= ~(1u << 1);

            harvestInstance.GlobalPosition = new Vector2(GlobalPosition.X, GlobalPosition.Y - FoodDropHeight);
            GetTree().Root.AddChild(harvestInstance);


            await ToSignal(GetTree().CreateTimer(0.8f), Timer.SignalName.Timeout);

            var tween = GetTree().CreateTween();
            tween.TweenProperty(harvestInstance, "position", GlobalPosition, 1.0f);
            tween.TweenProperty(harvestInstance, "scale", new Vector2(0.5f, 0.5f), 1.0f);
            tween.TweenCallback(Callable.From(harvestInstance.QueueFree));

            _inventoryManager.RemoveCollectable(_inventoryItem);
        }
    }

    private void AddRewardScene()
    {
        foreach (var scene in OutputRewardScenes)
        {
            var rewardInstance = scene.Instantiate<Node2D>();

            var rewardPosition = _rewardMarker2D.GlobalPosition;
            rewardPosition.X -= _currentXShift;
            rewardInstance.GlobalPosition = rewardPosition;

            GetTree().Root.AddChild(rewardInstance);
        }

        _currentXShift += RewardXShift;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed(InputConstants.ShowDialogue)
            && _isPlayerInRange)
        {
            _interactableLabelComponent.Visible = false;

            var dialogueBalloon = _dialogueBalloonScene.Instantiate<DialogueBalloon>();
            GetTree().Root.AddChild(dialogueBalloon);
            dialogueBalloon.Start(GD.Load<Resource>("res://Dialogues/AnimalHouseDialogue.dialogue"), InteractStartCommand);
        }
    }

    public override void _ExitTree()
    {
        _dialogueManager.FeedTheAnimals -= OnFeedTheAnimals;
        _interactableComponent.InteractableActivated -= OnInteractableActivated;
        _interactableComponent.InteractableDeactivated -= OnInteractableDeactivated;
        _feedComponent.FoodReceived -= OnFoodReceived;
    }
}
