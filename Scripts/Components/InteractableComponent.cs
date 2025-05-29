using CarrotCottage.Scripts.Objects;
using Godot;
using System;

namespace CarrotCottage.Scripts.Components;

public partial class InteractableComponent : Area2D
{
    [Signal]
    public delegate void InteractableActivatedEventHandler();

    [Signal]
    public delegate void InteractableDeactivatedEventHandler();

    public void OnBodyEntered(Node2D _)
    {
        EmitSignal(SignalName.InteractableActivated);
    }

    public void OnBodyExited(Node2D _)
    {
        EmitSignal(SignalName.InteractableDeactivated);
    }
}