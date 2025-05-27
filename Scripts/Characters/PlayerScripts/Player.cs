using Godot;
using System;

namespace CarrotCottage.Scripts.Characters.PlayerScripts;

public partial class Player : CharacterBody2D
{
    [Export]
    public PlayerTools CurrentTool { get; set; } = PlayerTools.None;
    public Vector2 CurrentDirection { get; set; }
}
