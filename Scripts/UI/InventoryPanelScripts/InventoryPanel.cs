using CarrotCottage.Scripts.Globals;
using Godot;
using System;
using System.Collections.Generic;

namespace CarrotCottage.Scripts.UI.InventoryPanelScripts;

public partial class InventoryPanel : PanelContainer
{
    private InventoryManager _inventoryManager = default!;

    private readonly Dictionary<StringName, Label> _collectableLabels = [];

    public override void _Ready()
    {
        _inventoryManager = GetNode<InventoryManager>(GlobalNames.InventoryManager);

        _collectableLabels["SmallLog"] = GetNode<Label>(InventoryPanelConstants.Nodes.SmallLogsLabel);
        _collectableLabels["LargeLog"] = GetNode<Label>(InventoryPanelConstants.Nodes.LargeLogsLabel);
        _collectableLabels["Stone"] = GetNode<Label>(InventoryPanelConstants.Nodes.StonesLabel);
        _collectableLabels["Egg"] = GetNode<Label>(InventoryPanelConstants.Nodes.EggsLabel);
        _collectableLabels["Milk"] = GetNode<Label>(InventoryPanelConstants.Nodes.MilkLabel);
        _collectableLabels["Wheat"] = GetNode<Label>(InventoryPanelConstants.Nodes.WheatLabel);
        _collectableLabels["Tomato"] = GetNode<Label>(InventoryPanelConstants.Nodes.TomatoesLabel);

        _inventoryManager.InventoryChanged += OnInventoryChanged;
    }

    private void OnInventoryChanged(StringName collectableKey, bool _)
    {
        if (!_inventoryManager.Inventory.TryGetValue(collectableKey, out int value))
        {
            throw new Exception("Unknow collectable collected.");
        }

        _collectableLabels[collectableKey].Text = value.ToString();
    }

    public override void _ExitTree()
    {
        _inventoryManager.InventoryChanged -= OnInventoryChanged;
    }
}
