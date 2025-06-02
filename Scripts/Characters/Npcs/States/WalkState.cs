using CarrotCottage.Scripts.StateMachine;
using Godot;
using System;

namespace CarrotCottage.Scripts.Characters.Npcs.States;

public partial class WalkState : NodeState
{
    [Export]
    public Npc Npc { get; set; } = default!;

    [Export]
    public AnimatedSprite2D AnimatedSprite2D { get; set; } = default!;

    [Export]
    public NavigationAgent2D NavigationAgent2D { get; set; } = default!;

    [Export]
    public float MinSpeed { get; set; } = 3f;

    [Export]
    public float MaxSpeed { get; set; } = 15f;

    private float _speed;

    public override void _Ready()
    {
        NavigationAgent2D.VelocityComputed += OnVelocityComputed;
    }

    private void SetMovementTarget()
    {
        var targetPosition = NavigationServer2D.MapGetRandomPoint(map: NavigationAgent2D.GetNavigationMap(), 
            navigationLayers: NavigationAgent2D.NavigationLayers, 
            uniformly: false);

        NavigationAgent2D.TargetPosition = targetPosition;
        _speed = (float)GD.RandRange(MinSpeed, MaxSpeed);
    }

    public override void OnProcess(double delta)
    {

    }

    public override void OnPhysicsProcess(double delta)
    {
        if (NavigationAgent2D.IsNavigationFinished())
        {
            ++Npc.CurrentWalkCycle;
            SetMovementTarget();

            return;
        }

        var targetPosition = NavigationAgent2D.GetNextPathPosition();
        var targetDirection = Npc.GlobalPosition.DirectionTo(targetPosition);

        var velocity = targetDirection * _speed;

        if (NavigationAgent2D.AvoidanceEnabled)
        {
            NavigationAgent2D.Velocity = velocity;
        }
        else
        {
            AnimatedSprite2D.FlipH = targetDirection.X < 0;
            Npc.Velocity = velocity;
        }

        Npc.MoveAndSlide();
    }

    public override void OnNextTransition()
    {
        if (Npc.WalkCycles == Npc.CurrentWalkCycle)
        {
            Npc.Velocity = Vector2.Zero;
            EmitSignal(SignalName.Transition, NpcConstants.States.Idle);
        }
    }

    public override void OnEnter()
    {
        Npc.WalkCycles = GD.RandRange(Npc.MinWalkCycles, Npc.MaxWalkCycles);
        Npc.CurrentWalkCycle = 0;

        SetMovementTarget();

        AnimatedSprite2D.Play(NpcConstants.Animations.Walk);

        Npc.CurrentStateName = NpcConstants.States.Walk;
    }

    public override void OnExit()
    {
        Npc.IsFlippedH = AnimatedSprite2D.FlipH;
        AnimatedSprite2D.Stop();
    }

    private void OnVelocityComputed(Vector2 safeVelocity)
    {
        if (Npc.CurrentStateName == NpcConstants.States.Walk)
        {
            AnimatedSprite2D.FlipH = safeVelocity.X < 0;
            Npc.Velocity = safeVelocity;
        }
    }
}
