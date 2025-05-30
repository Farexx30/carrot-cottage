using CarrotCottage.Scripts.Characters.PlayerScripts;
using Godot;

namespace CarrotCottage.Scripts.Components;

public partial class HitComponent : Area2D
{
    [Export]
    public PlayerTools CurrentTool { get; set; } = PlayerTools.None;

    [Export]
    public int Damage { get; set; } = 1;
}
