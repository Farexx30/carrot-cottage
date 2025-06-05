using CarrotCottage.Scripts.Common;
using CarrotCottage.Scripts.Components;
using CarrotCottage.Scripts.Globals;
using Godot;
using System;

namespace CarrotCottage.Scripts.Characters.PlayerScripts;

public partial class Player : CharacterBody2D
{
    [Export]
    public PlayerTools CurrentTool { get; set; } = PlayerTools.None;
    public Vector2 CurrentDirection { get; set; }

    private ToolsManager _toolsManager = default!;
    private HitComponent _hitComponent = default!;

    public override void _Ready()
    {
        _toolsManager = GetNode<ToolsManager>(GlobalNames.ToolsManager);
        _hitComponent = GetNode<HitComponent>(ComponentNames.HitComponent);

        _toolsManager.ToolChanged += OnToolChanged;
    }

    private void OnToolChanged(PlayerTools newTool)
    {
        CurrentTool = newTool;
        GD.Print($"Current tool changed to: {CurrentTool}");

        _hitComponent.CurrentTool = newTool;
    }

    public override void _ExitTree()
    {
        _toolsManager.ToolChanged -= OnToolChanged;
    }
}
