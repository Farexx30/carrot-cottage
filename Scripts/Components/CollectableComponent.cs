using CarrotCottage.Scripts.Characters.PlayerScripts;
using CarrotCottage.Scripts.Globals;
using Godot;

namespace CarrotCottage.Scripts.Components;

public partial class CollectableComponent : Area2D
{
    [Export]
    public StringName CollectableName { get; set; } = default!;

    private InventoryManager _inventoryManager = default!;

    public override void _Ready()
    {
        _inventoryManager = GetNode<InventoryManager>(GlobalNames.InventoryManager);
    }

    public void OnBodyEntered(Node2D body)
    {
        if (body is Player)
        {
            _inventoryManager.AddCollectable(CollectableName);

            GD.Print($"{CollectableName} collected.");
            
            GetParent().QueueFree();
        }
    }
}
