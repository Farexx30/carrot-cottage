using CarrotCottage.Scripts.Characters.PlayerScripts.Inputs;
using CarrotCottage.Scripts.Components;
using CarrotCottage.Scripts.Dialogues;
using Godot;
using Godot.Collections;
using System;

namespace CarrotCottage.Scripts.Objects.ChestScripts;

public partial class Chest : Node2D
{
    private readonly PackedScene _dialogueBalloonScene = GD.Load<PackedScene>("res://Scenes/Dialogues/DialogueBalloon.tscn");

    private readonly PackedScene _wheatHarvestScene = GD.Load<PackedScene>("res://Scenes/Objects/Plants/WheatHarvest.tscn");
    private readonly PackedScene _tomatoHarvestScene = GD.Load<PackedScene>("res://Scenes/Objects/Plants/WheatHarvest.tscn");

    [Export]
    public StringName InteractStartCommand { get; set; } = default!;

    [Export]
    public int FoodDropHeight { get; set; } = 40;

    [Export]
    public int RewardOutputRadius { get; set; } = 20;

    [Export]
    public Array<PackedScene> OutputRewardScenes { get; set; } = [];

    private InteractableComponent _interactableComponent = default!; 
    private Control _interactableLabelComponent = default!; 
    private FeedComponent _feedComponent = default!; 
    private AnimatedSprite2D _animatedSprite2D = default!; 
    private Marker2D _rewardMarker2D = default!;

    private bool _isPlayerInRange = false;
    private bool _isOpen = false;

    public override void _Ready()
    {
        _interactableComponent = GetNode<InteractableComponent>(ChestConstants.Nodes.InteractableComponent);
        _interactableLabelComponent = GetNode<Control>(ChestConstants.Nodes.InteractableLabelComponent);
        _feedComponent = GetNode<FeedComponent>(ChestConstants.Nodes.FeedComponent);
        _animatedSprite2D = GetNode<AnimatedSprite2D>(ChestConstants.Nodes.AnimatedSprite2D);
        _rewardMarker2D = GetNode<Marker2D>(ChestConstants.Nodes.RewardMarker2D);

        _interactableLabelComponent.Visible = false;

        _interactableComponent.InteractableActivated += OnInteractableActivated;
        _interactableComponent.InteractableDeactivated += OnInteractableDeactivated;
        _feedComponent.FoodReceived += OnFoodReceived;
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
        throw new NotImplementedException();
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
