using CarrotCottage.Scripts.Characters.PlayerScripts;
using CarrotCottage.Scripts.Characters.PlayerScripts.Inputs;
using CarrotCottage.Scripts.Common;
using CarrotCottage.Scripts.Globals;
using Godot;
using System;
using System.Collections.Generic;

namespace CarrotCottage.Scripts.UI.ToolsPanelScripts;

public partial class ToolsPanel : PanelContainer
{
    private ToolsManager _toolsManager = default!;

    private Button _axeTool = default!;
    private Button _hoeTool = default!;
    private Button _wateringCanTool = default!;
    private Button _wheatSeedsTool = default!;
    private Button _tomatoSeedsTool = default!;

    private Button? _selectedToolButton;

    private readonly Dictionary<PlayerTools, Button> _toolButtons = [];

    public override void _Ready()
    {
        _toolsManager = GetNode<ToolsManager>(GlobalNames.ToolsManager);

        _axeTool = GetNode<Button>(ToolsPanelConstants.Nodes.AxeTool);
        _hoeTool = GetNode<Button>(ToolsPanelConstants.Nodes.HoeTool);
        _wateringCanTool = GetNode<Button>(ToolsPanelConstants.Nodes.WateringCanTool);
        _wheatSeedsTool = GetNode<Button>(ToolsPanelConstants.Nodes.WheatSeedsTool);
        _tomatoSeedsTool = GetNode<Button>(ToolsPanelConstants.Nodes.TomatoSeedsTool);

        _toolButtons.Add(PlayerTools.Axe, _axeTool);
        _toolButtons.Add(PlayerTools.Hoe, _hoeTool);
        _toolButtons.Add(PlayerTools.WateringCan, _wateringCanTool);
        _toolButtons.Add(PlayerTools.WheatSeeds, _wheatSeedsTool);
        _toolButtons.Add(PlayerTools.TomatoSeeds, _tomatoSeedsTool);

        foreach (var toolButton in _toolButtons)
        {
            if (toolButton.Key != PlayerTools.Axe)
            {
                toolButton.Value.Disabled = true;
                toolButton.Value.FocusMode = FocusModeEnum.None;
            }
        }

        _toolsManager.ToolEnabled += OnToolEnabled;
    }

    private void OnToolEnabled(PlayerTools tool)
    {
        if (_toolButtons.TryGetValue(tool, out var toolButton))
        {
            toolButton.Disabled = false;
            toolButton.FocusMode = FocusModeEnum.All;
        }
    }

    private void OnAxeToolPressed()
    {
        _toolsManager.ChangeTool(PlayerTools.Axe);
        _selectedToolButton = _axeTool;
    }

    private void OnHoeToolPressed()
    {
        _toolsManager.ChangeTool(PlayerTools.Hoe);
        _selectedToolButton = _hoeTool;
    }

    private void OnWateringCanToolPressed()
    {
        _toolsManager.ChangeTool(PlayerTools.WateringCan);
        _selectedToolButton = _wateringCanTool;
    }

    private void OnWheatSeedsToolPressed()
    {
        _toolsManager.ChangeTool(PlayerTools.WheatSeeds);
        _selectedToolButton = _wheatSeedsTool;
    }

    private void OnTomatoSeedsToolPressed()
    {
        _toolsManager.ChangeTool(PlayerTools.TomatoSeeds);
        _selectedToolButton = _tomatoSeedsTool;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed(InputConstants.ReleaseTool)
            && _selectedToolButton is not null)
        {
            _toolsManager.ChangeTool(PlayerTools.None);

            _selectedToolButton.ReleaseFocus();
            _selectedToolButton = null;
        }
    }

    public override void _ExitTree()
    {
        _toolsManager.ToolEnabled -= OnToolEnabled;
    }
}