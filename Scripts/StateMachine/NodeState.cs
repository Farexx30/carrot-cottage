using Godot;
using System;

namespace CarrotCottage.Scripts.StateMachine;

public partial class NodeState : Node
{
    [Signal]
    public delegate void TransitionEventHandler(StringName nodeStateName);

    public virtual void OnProcess(double delta)
    {
        // This method can be overridden in derived classes to implement specific behavior
        // during the process phase of the state.
    }

    public virtual void OnPhysicsProcess(double delta)
    {
        // This method can be overridden in derived classes to implement specific behavior
        // during the physics process phase of the state.
    }

    public virtual void OnNextTransition()
    {
        // This method can be overridden in derived classes to implement specific behavior
        // when transitioning to the next state.
    }

    public virtual void OnEnter()
    {
        // This method can be overridden in derived classes to implement specific behavior
        // when entering the state.
    }

    public virtual void OnExit()
    {
        // This method can be overridden in derived classes to implement specific behavior
        // when exiting the state.
    }
}
