using CarrotCottage.Scripts.Characters.PlayerScripts;
using Godot;

namespace CarrotCottage.Scripts.Components;

public partial class CollectableComponent : Area2D
{
    [Export]
    public string CollectableName { get; set; } = default!;

    public void OnBodyEntered(Node2D body)
    {
        if (body is Player)
        {
            GD.Print($"{CollectableName} collected.");
            
            GetParent().QueueFree();
        }
    }
}
