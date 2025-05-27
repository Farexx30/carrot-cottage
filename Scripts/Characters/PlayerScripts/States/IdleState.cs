using CarrotCottage.Scripts.Characters.PlayerScripts.Inputs;
using CarrotCottage.Scripts.StateMachine;
using Godot;
using System;

namespace CarrotCottage.Scripts.Characters.PlayerScripts.States;

public partial class IdleState : NodeState
{
    [Export]
    public Player Player { get; set; } = default!;

    [Export]
    public AnimatedSprite2D AnimatedSprite2D { get; set; } = default!;

    public override void OnProcess(double delta)
    {
        
    }

    public override void OnPhysicsProcess(double delta)
    {
        if (Player.CurrentDirection == Vector2.Up)
        {
            AnimatedSprite2D.Play(PlayerConstants.Animations.IdleBack);
        }
        else if (Player.CurrentDirection == Vector2.Down)
        {
            AnimatedSprite2D.Play(PlayerConstants.Animations.IdleFront);
        }
        else if (Player.CurrentDirection == Vector2.Left)
        {
            AnimatedSprite2D.Play(PlayerConstants.Animations.IdleLeft);
        }
        else if (Player.CurrentDirection == Vector2.Right)
        {
            AnimatedSprite2D.Play(PlayerConstants.Animations.IdleRight);
        }
    }

    public override void OnNextTransition()
    {
        _ = InputEvents.MovementInputDirection();

        if (InputEvents.IsMovementInput())
        {
            EmitSignal(SignalName.Transition, PlayerConstants.States.Walk);
        }
    }

    public override void OnEnter()
    {

    }

    public override void OnExit()
    {
        AnimatedSprite2D.Stop();
    }
}
