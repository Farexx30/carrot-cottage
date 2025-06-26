using CarrotCottage.Scripts.Characters.PlayerScripts;
using CarrotCottage.Scripts.Characters.PlayerScripts.Inputs;
using CarrotCottage.Scripts.StateMachine;
using Godot;
using System;

namespace CarrotCottage.Scripts.Characters.Npcs.States;

public partial class IdleState : NodeState
{
    [Export]
    public Npc Npc { get; set; } = default!;

    [Export]
    public AnimatedSprite2D AnimatedSprite2D { get; set; } = default!;

    [Export]
    public Timer IdleStateTimer { get; set; } = default!;

    private bool _isIdleStateTimerTimeout = false;

    public override void _Ready()
    {
        Npc.IsFlippedH = AnimatedSprite2D.FlipH;

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
        AnimatedSprite2D.FlipH = Npc.IsFlippedH;

        _isIdleStateTimerTimeout = false;

        var idleStateWaitTime = (float)GD.RandRange(Npc.MinIdleStateTime, Npc.MaxIdleStateTime);
        IdleStateTimer.WaitTime = idleStateWaitTime;
        IdleStateTimer.Start();

        Npc.CurrentStateName = NpcConstants.States.Idle;
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
