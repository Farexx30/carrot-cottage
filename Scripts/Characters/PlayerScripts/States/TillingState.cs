using CarrotCottage.Scripts.StateMachine;
using Godot;

namespace CarrotCottage.Scripts.Characters.PlayerScripts.States;

public partial class TillingState : NodeState
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
        if (!AnimatedSprite2D.IsPlaying())
        {
            EmitSignal(SignalName.Transition, PlayerConstants.States.Idle);
        }
    }

    public override void OnEnter()
    {
        if (Player.CurrentDirection == Vector2.Up)
        {
            AnimatedSprite2D.Play(PlayerConstants.Animations.TillingBack);
        }
        else if (Player.CurrentDirection == Vector2.Down
                 || Player.CurrentDirection == Vector2.Zero)
        {
            AnimatedSprite2D.Play(PlayerConstants.Animations.TillingFront);
        }
        else if (Player.CurrentDirection == Vector2.Left)
        {
            AnimatedSprite2D.Play(PlayerConstants.Animations.TillingLeft);
        }
        else if (Player.CurrentDirection == Vector2.Right)
        {
            AnimatedSprite2D.Play(PlayerConstants.Animations.TillingRight);
        }
    }

    public override void OnExit()
    {
        AnimatedSprite2D.Stop();
    }
}
