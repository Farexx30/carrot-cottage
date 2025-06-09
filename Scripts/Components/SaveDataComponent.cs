using CarrotCottage.Scripts.Resources;
using Godot;
using System;

namespace CarrotCottage.Scripts.Components;

public partial class SaveDataComponent : Node
{
    private Node2D _parentNode = default!;

    [Export]
    public NodeDataResource SaveDataResource { get; set; } = default!;

    public override void _Ready()
    {
        _parentNode = GetParent<Node2D>();

        AddToGroup(nameof(SaveDataComponent));
    }

    public virtual NodeDataResource? SaveData()
    {
        if (_parentNode is null)
        {
            GD.PushError("Parent node is null. Cannot save data.");
            return null;
        }

        if (SaveDataResource is null)
        {
            GD.PushError("SaveDataResource is not set. Cannot save data.");
            return null;
        }

        SaveDataResource.SaveData(_parentNode);

        return SaveDataResource;
    }
}
