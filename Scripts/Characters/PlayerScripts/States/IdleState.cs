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

    }

    public override void OnNextTransition()
    {
        _ = InputEvents.MovementInputDirection();

        if (InputEvents.IsMovementInput())
        {
            EmitSignal(SignalName.Transition, PlayerConstants.States.Walk);
        }

        if (Player.CurrentTool == PlayerTools.Axe && InputEvents.IsHitInput())
        {
            EmitSignal(SignalName.Transition, PlayerConstants.States.Chopping);
        }
    }

    public override void OnEnter()
    {
        if (Player.CurrentDirection == Vector2.Up)
        {
            AnimatedSprite2D.Play(PlayerConstants.Animations.IdleBack);
        }
        else if (Player.CurrentDirection == Vector2.Down
                 || Player.CurrentDirection == Vector2.Zero)
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

    public override void OnExit()
    {
        AnimatedSprite2D.Stop();
    }
}
