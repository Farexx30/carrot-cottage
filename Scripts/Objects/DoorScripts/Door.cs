using CarrotCottage.Scripts.Components;
using Godot;
using System;

namespace CarrotCottage.Scripts.Objects.DoorScripts;

public partial class Door : StaticBody2D
{
    private AnimatedSprite2D _animatedSprite2D = default!;
    private CollisionShape2D _collisionShape2D = default!;
    private InteractableComponent _interactableComponent = default!;

    private const int FrameIndexToDisableCollision = 4; // Frame at which the door collision shape should be disabled.
    private const int FrameIndexToEnableCollision = 3; // Frame at which the door collision shape should be enabled.

    private bool _isOpening = false;

    public override void _Ready()
    {
        _animatedSprite2D = GetNode<AnimatedSprite2D>(DoorConstants.Nodes.AnimatedSprite2D);
        _collisionShape2D = GetNode<CollisionShape2D>(DoorConstants.Nodes.CollisionShape2D);
        _interactableComponent = GetNode<InteractableComponent>(ComponentNames.InteractableComponent);

        _interactableComponent.InteractableActivated += OnInteractableActivated;
        _interactableComponent.InteractableDeactivated += OnInteractableDeactivated;
    }

    private void OnInteractableActivated()
    {
        _animatedSprite2D.Play(DoorConstants.Animations.OpeningOrClosing);
        _isOpening = true;

        GD.Print($"ACTIVATING: Current frame: {_animatedSprite2D.Frame}");
    }

    private void OnInteractableDeactivated()
    {
        _animatedSprite2D.PlayBackwards(DoorConstants.Animations.OpeningOrClosing);
        _isOpening = false;

        GD.Print($"DEACTIVATING: Current frame: {_animatedSprite2D.Frame}");
    }

    private void OnAnimatedSprite2DFrameChanged()
    {
        GD.Print($"Frame changed: {_animatedSprite2D.Frame}");

        if (_animatedSprite2D.Animation == DoorConstants.Animations.OpeningOrClosing)
        {
            if (_isOpening && _animatedSprite2D.Frame == FrameIndexToDisableCollision)
            {
                _collisionShape2D.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
            }
            else if (!_isOpening && _animatedSprite2D.Frame == FrameIndexToEnableCollision)
            {
                _collisionShape2D.SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
            }
        }
    }

    public override void _ExitTree()
    {
        // Unsubscribe from signals to prevent memory leaks although it's not strictly necessary in this case since it should be freed automatically.
        _interactableComponent.InteractableActivated -= OnInteractableActivated;
        _interactableComponent.InteractableDeactivated -= OnInteractableDeactivated;
    }
}
