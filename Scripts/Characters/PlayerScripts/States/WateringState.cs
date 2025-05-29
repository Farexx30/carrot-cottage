using CarrotCottage.Scripts.StateMachine;
using Godot;

namespace CarrotCottage.Scripts.Characters.PlayerScripts.States;

public partial class WateringState : NodeState
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
            AnimatedSprite2D.Play(PlayerConstants.Animations.WateringBack);
        }
        else if (Player.CurrentDirection == Vector2.Down
                 || Player.CurrentDirection == Vector2.Zero)
        {
            AnimatedSprite2D.Play(PlayerConstants.Animations.WateringFront);
        }
        else if (Player.CurrentDirection == Vector2.Left)
        {
            AnimatedSprite2D.Play(PlayerConstants.Animations.WateringLeft);
        }
        else if (Player.CurrentDirection == Vector2.Right)
        {
            AnimatedSprite2D.Play(PlayerConstants.Animations.WateringRight);
        }
    }

    public override void OnExit()
    {
        AnimatedSprite2D.Stop();
    }
}
