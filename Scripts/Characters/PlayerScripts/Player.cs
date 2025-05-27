using Godot;
using System;

namespace CarrotCottage.Scripts.Characters.PlayerScripts;

public partial class Player : CharacterBody2D
{
    public Vector2 CurrentDirection { get; set; }
}
