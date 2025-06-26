using CarrotCottage.Scripts.Common;
using CarrotCottage.Scripts.Globals;
using Godot;
using System;

namespace CarrotCottage.Scripts.Components;

public partial class EnableToolsPanelComponent : Node2D
{
    private ToolsManager _toolsManager = default!;

    public override void _Ready()
    {
        _toolsManager = GetNode<ToolsManager>(GlobalNames.ToolsManager);

        CallDeferred(nameof(EnableToolsPanel));
    }

    private void EnableToolsPanel()
    {
        _toolsManager.EnableTool(PlayerTools.Axe);
        _toolsManager.EnableTool(PlayerTools.Hoe);
        _toolsManager.EnableTool(PlayerTools.WateringCan);
        _toolsManager.EnableTool(PlayerTools.WheatSeeds);
        _toolsManager.EnableTool(PlayerTools.TomatoSeeds);
    }
}
