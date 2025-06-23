using Godot;
using System;
using System.Collections.Generic;

namespace CarrotCottage.Scripts.Globals;

public partial class InventoryManager : Node
{
    [Signal]
    public delegate void InventoryChangedEventHandler(StringName collectableKey);

    public Dictionary<StringName, int> Inventory { get; } = [];

    public void AddCollectable(StringName collectableName)
    {
        if (!Inventory.TryGetValue(collectableName, out int value))
        {
            Inventory[collectableName] = value;
        }

        Inventory[collectableName] = value + 1;

        GD.Print($"{collectableName} added to inventory. Total: {Inventory[collectableName]}");

        EmitSignal(SignalName.InventoryChanged, collectableName);
    }

    public void RemoveCollectable(StringName collectableName, int quantity = 1)
    {
        if (Inventory.TryGetValue(collectableName, out int value) && value > 0)
        {
            Inventory[collectableName] = value - quantity;
            GD.Print($"{quantity} {collectableName} removed from inventory. Current count: {Inventory[collectableName]}");
            EmitSignal(SignalName.InventoryChanged, collectableName);
        }
    }
}
