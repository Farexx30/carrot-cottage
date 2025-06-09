using Godot;

namespace CarrotCottage.Scripts.Resources;

public partial class SceneDataResource : NodeDataResource
{
    [Export]
    public StringName NodeName { get; set; } = default!;

    [Export]
    public StringName SceneFilePath { get; set; } = default!;


    public override void SaveData(Node2D node)
    {
        base.SaveData(node);

        NodeName = node.Name;
        SceneFilePath = node.GetSceneFilePath();
    }


    public override void LoadData(Window window)
    {
        Node2D? parentNode = null;         
        if (ParentNodePath is not null)
        {
            parentNode = window.GetNodeOrNull<Node2D>(ParentNodePath);
        }

        Node2D? sceneNode = null;
        if (NodePath is not null)
        {
            var sceneFileResource = GD.Load<PackedScene>(SceneFilePath);
            sceneNode = sceneFileResource?.Instantiate<Node2D>();
        }

        if (parentNode is not null && sceneNode is not null)
        {
            sceneNode.GlobalPosition = GlobalPosition;
            parentNode.AddChild(sceneNode);
        }
    }
}