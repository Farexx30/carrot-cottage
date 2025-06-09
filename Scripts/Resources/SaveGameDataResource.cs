using Godot;
using Godot.Collections;

namespace CarrotCottage.Scripts.Resources;

public partial class SaveGameDataResource : Resource
{
    [Export]
    public Array<NodeDataResource> SaveDataNodes { get; set; } = [];
}