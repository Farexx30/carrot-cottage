using CarrotCottage.Scripts.Components;
using Godot;
using System;

namespace CarrotCottage.Scripts.Objects.DoorScripts;

public partial class Door : StaticBody2D
{
    private AnimatedSprite2D _animatedSprite2D = default!;
    private CollisionShape2D _collisionShape2D = default!;
    private InteractableComponent _interactableComponent = default!;

    public override void _Ready()
    {
        _animatedSprite2D = GetNode<AnimatedSprite2D>(DoorConstants.Nodes.AnimatedSprite2D);
        _collisionShape2D = GetNode<CollisionShape2D>(DoorConstants.Nodes.CollisionShape2D);
        _interactableComponent = GetNode<InteractableComponent>(DoorConstants.Nodes.InteractableComponent);

        _interactableComponent.InteractableActivated += OnInteractableActivated;
        _interactableComponent.InteractableDeactivated += OnInteractableDeactivated;
    }

    private void OnInteractableActivated()
    {
        _animatedSprite2D.Play(DoorConstants.Animations.Opening);

        _collisionShape2D.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
    }

    private void OnInteractableDeactivated()
    {
        _animatedSprite2D.Play(DoorConstants.Animations.Closing);

        _collisionShape2D.SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
    }
}
