using Godot;
using System;

namespace CarrotCottage.Scripts.Characters.Npcs;

public partial class Npc : CharacterBody2D
{
    [Export]
    public float MinIdleStateTime { get; set; } = 5f;

    [Export]
    public float MaxIdleStateTime { get; set; } = 15f;

    [Export]
    public int MinWalkCycles { get; set; } = 1;

    [Export]
    public int MaxWalkCycles { get; set; } = 5;

    public int WalkCycles { get; set; }
    public int CurrentWalkCycle { get; set; }

    public StringName CurrentStateName { get; set; } = NpcConstants.States.Idle;

    public bool IsFlippedH { get; set; }
}
