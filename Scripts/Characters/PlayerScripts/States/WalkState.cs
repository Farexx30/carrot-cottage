using CarrotCottage.Scripts.Characters.PlayerScripts.Inputs;
using CarrotCottage.Scripts.StateMachine;
using Godot;
using System;

namespace CarrotCottage.Scripts.Characters.PlayerScripts.States;
public partial class WalkState : NodeState
{
    [Export]
    public Player Player { get; set; } = default!;

    [Export]
    public AnimatedSprite2D AnimatedSprite2D { get; set; } = default!;

    [Export]
    public float Speed { get; set; } = 60f;

    public override void OnProcess(double delta)
    {
        
    }

    public override void OnPhysicsProcess(double delta)
    {
        var direction = InputEvents.MovementInputDirection();

        if (direction == Vector2.Up)
        {
            AnimatedSprite2D.Play(PlayerConstants.Animations.WalkBack);
        }
        else if (direction == Vector2.Down)
        {
            AnimatedSprite2D.Play(PlayerConstants.Animations.WalkFront);
        }
        else if (direction == Vector2.Left)
        {
            AnimatedSprite2D.Play(PlayerConstants.Animations.WalkLeft);
        }
        else if (direction == Vector2.Right)
        {
            AnimatedSprite2D.Play(PlayerConstants.Animations.WalkRight);
        }

        if (direction != Vector2.Zero)
        {
            Player.CurrentDirection = direction;
        }

        Player.Velocity = direction * Speed;
        Player.MoveAndSlide();
    }

    public override void OnNextTransition()
    {
        if (!InputEvents.IsMovementInput())
        {
            EmitSignal(SignalName.Transition, PlayerConstants.States.Idle);
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
