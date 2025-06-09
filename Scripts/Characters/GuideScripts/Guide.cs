using CarrotCottage.Scripts.Components;
using Godot;
using System;

namespace CarrotCottage.Scripts.Characters.GuideScripts;

public partial class Guide : Node2D
{
    private InteractableComponent _interactableComponent = default!;
    private Control _interactableLabelComponent = default!;

    public override void _Ready()
    {
        _interactableComponent = GetNode<InteractableComponent>(GuideConstants.Nodes.InteractableComponent);
        _interactableLabelComponent = GetNode<Control>(GuideConstants.Nodes.InteractableLabelComponent);

        _interactableComponent.InteractableActivated += OnInteractableActivated;
        _interactableComponent.InteractableDeactivated += OnInteractableDeactivated;

        _interactableLabelComponent.Visible = false;
    }

    private void OnInteractableActivated()
    {
        _interactableLabelComponent.Visible = true;
    }

    private void OnInteractableDeactivated()
    {
        _interactableLabelComponent.Visible = false;
    }
}
