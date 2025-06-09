using Godot;

namespace CarrotCottage.Scripts.Resources;

public partial class NodeDataResource : Resource
{
    [Export]
    public Vector2 GlobalPosition { get; set; } = Vector2.Zero;

    [Export]
    public NodePath NodePath { get; set; } = default!;

    [Export]
    public NodePath? ParentNodePath { get; set; }


    public virtual void SaveData(Node2D node)
    {
        GlobalPosition = node.GlobalPosition;
        NodePath = node.GetPath();
        ParentNodePath = node.GetParent()?.GetPath();
    }

    public virtual void LoadData(Window window)
    {
        
    }
}