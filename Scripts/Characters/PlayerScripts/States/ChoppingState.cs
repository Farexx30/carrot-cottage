using CarrotCottage.Scripts.StateMachine;
using Godot;

namespace CarrotCottage.Scripts.Characters.PlayerScripts.States;

public partial class ChoppingState : NodeState
{
    [Export]
    public Player Player { get; set; } = default!;

    [Export]
    public AnimatedSprite2D AnimatedSprite2D { get; set; } = default!;

    [Export]
    public CollisionShape2D HitComponentCollisionShape { get; set; } = default!;

    public override void _Ready()
    {
        HitComponentCollisionShape.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
        HitComponentCollisionShape.Position = Vector2.Zero;
    }

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
            AnimatedSprite2D.Play(PlayerConstants.Animations.ChoppingBack);
            HitComponentCollisionShape.Position = new Vector2(3, -19);
        }
        else if (Player.CurrentDirection == Vector2.Down
                 || Player.CurrentDirection == Vector2.Zero)
        {
            AnimatedSprite2D.Play(PlayerConstants.Animations.ChoppingFront);
            HitComponentCollisionShape.Position = new Vector2(-3, 3);
        }
        else if (Player.CurrentDirection == Vector2.Left)
        {
            AnimatedSprite2D.Play(PlayerConstants.Animations.ChoppingLeft);
            HitComponentCollisionShape.Position = new Vector2(-8, -1);
        }
        else if (Player.CurrentDirection == Vector2.Right)
        {
            AnimatedSprite2D.Play(PlayerConstants.Animations.ChoppingRight);
            HitComponentCollisionShape.Position = new Vector2(8, -1);
        }

        HitComponentCollisionShape.SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
    }

    public override void OnExit()
    {
        AnimatedSprite2D.Stop();
        HitComponentCollisionShape.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
    }
}
