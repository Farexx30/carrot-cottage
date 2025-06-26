using CarrotCottage.Scripts.Resources;
using Godot;

namespace CarrotCottage.Scripts.Resources;

public partial class ToolsPanelResource : NodeDataResource
{
    public override void LoadData(Window window)
    {
        if (NodePath is null)
        {
            return;
        }

        var parent = window.GetNodeOrNull(ParentNodePath);
        var instance = GD.Load<PackedScene>("res://Scenes/Components/EnableToolsPanelComponent.tscn").Instantiate();

        parent?.AddChild(instance);
    }
}