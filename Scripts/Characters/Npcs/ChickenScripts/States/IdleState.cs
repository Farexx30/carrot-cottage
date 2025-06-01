using CarrotCottage.Scripts.Characters.PlayerScripts;
using CarrotCottage.Scripts.Characters.PlayerScripts.Inputs;
using CarrotCottage.Scripts.StateMachine;
using Godot;
using System;

namespace CarrotCottage.Scripts.Characters.Npcs.ChickenScripts.States;

public partial class IdleState : NodeState
{
    [Export]
    public Npc Chicken { get; set; } = default!;

    [Export]
    public AnimatedSprite2D AnimatedSprite2D { get; set; } = default!;

    [Export]
    public Timer IdleStateTimer { get; set; } = default!;

    private bool _isIdleStateTimerTimeout = false;

    public override void _Ready()
    {
        IdleStateTimer.Timeout += OnIdleTimerTimeout;
    }

    public override void OnProcess(double delta)
    {

    }

    public override void OnPhysicsProcess(double delta)
    {

    }

    public override void OnNextTransition()
    {
        if (_isIdleStateTimerTimeout)
        {
            EmitSignal(SignalName.Transition, NpcConstants.States.Walk);
        }
    }

    public override void OnEnter()
    {
        AnimatedSprite2D.Play(NpcConstants.Animations.Idle);
        AnimatedSprite2D.FlipH = Chicken.IsFlippedH;

        _isIdleStateTimerTimeout = false;

        var idleStateWaitTime = (float)GD.RandRange(Chicken.MinIdleStateTime, Chicken.MaxIdleStateTime);
        IdleStateTimer.WaitTime = idleStateWaitTime;
        IdleStateTimer.Start();

        Chicken.CurrentStateName = NpcConstants.States.Idle;
    }

    public override void OnExit()
    {
        AnimatedSprite2D.Stop();
    }

    private void OnIdleTimerTimeout()
    {
        _isIdleStateTimerTimeout = true;
    }
}
