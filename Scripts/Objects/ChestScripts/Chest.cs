using CarrotCottage.Scripts.Characters.PlayerScripts.Inputs;
using CarrotCottage.Scripts.Components;
using CarrotCottage.Scripts.Dialogues;
using CarrotCottage.Scripts.Globals;
using Godot;
using Godot.Collections;
using System;
using System.Threading.Tasks;

namespace CarrotCottage.Scripts.Objects.ChestScripts;

public partial class Chest : Node2D
{
    private readonly PackedScene _dialogueBalloonScene = GD.Load<PackedScene>("res://Scenes/Dialogues/DialogueBalloon.tscn");

    [Export]
    public StringName InteractStartCommand { get; set; } = default!;

    [Export]
    public int FoodDropHeight { get; set; } = 40;

    [Export]
    public int RewardOutputRadius { get; set; } = 20;

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
    private AnimatedSprite2D _animatedSprite2D = default!; 
    private Marker2D _rewardMarker2D = default!;

    private bool _isPlayerInRange = false;
    private bool _isOpen = false;

    public override void _Ready()
    {
        _dialogueManager = GetNode<CarrotCottageDialogueManager>(GlobalNames.DialogueManager);
        _inventoryManager = GetNode<InventoryManager>(GlobalNames.InventoryManager);
        _interactableComponent = GetNode<InteractableComponent>(ChestConstants.Nodes.InteractableComponent);
        _interactableLabelComponent = GetNode<Control>(ChestConstants.Nodes.InteractableLabelComponent);
        _feedComponent = GetNode<FeedComponent>(ChestConstants.Nodes.FeedComponent);
        _animatedSprite2D = GetNode<AnimatedSprite2D>(ChestConstants.Nodes.AnimatedSprite2D);
        _rewardMarker2D = GetNode<Marker2D>(ChestConstants.Nodes.RewardMarker2D);

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

        if (_isOpen)
        {
            _animatedSprite2D.PlayBackwards(ChestConstants.Animations.OpeningOrClosing);
            _isOpen = false;
        }
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
        foreach(var scene in OutputRewardScenes)
        {
            var rewardInstance = scene.Instantiate<Node2D>();

            var rewardPosition = GetRandomPositionInCircle(_rewardMarker2D.GlobalPosition, RewardOutputRadius);
            rewardInstance.GlobalPosition = rewardPosition;
            GetTree().Root.AddChild(rewardInstance);
        }
    }

    private Vector2I GetRandomPositionInCircle(Vector2 center, int r)
    {
        var angle = GD.Randf() * Math.Tau;

        var distanceFromCenter = Math.Sqrt(GD.Randf()) * r;

        var x = center.X + distanceFromCenter * Math.Cos(angle);
        var y = center.Y + distanceFromCenter * Math.Sin(angle);

        return new Vector2I((int)x, (int)y);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed(InputConstants.ShowDialogue)
            && _isPlayerInRange)
        {
            _interactableLabelComponent.Visible = false;
            _animatedSprite2D.Play(ChestConstants.Animations.OpeningOrClosing);
            _isOpen = true;

            var dialogueBalloon = _dialogueBalloonScene.Instantiate<DialogueBalloon>();
            GetTree().CurrentScene.AddChild(dialogueBalloon);
            dialogueBalloon.Start(GD.Load<Resource>("res://Dialogues/ChestDialogue.dialogue"), InteractStartCommand);
        }
    }

    public override void _ExitTree()
    {
        _interactableComponent.InteractableActivated -= OnInteractableActivated;
        _interactableComponent.InteractableDeactivated -= OnInteractableDeactivated;
        _feedComponent.FoodReceived -= OnFoodReceived;
    }
}
