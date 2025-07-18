using Godot;

namespace CarrotCottage.Scripts.Objects.TreeScripts;

public class TreeConstants
{
    public static class Nodes
    {
        public const string HitSFX = "AxeHitTreeSfx";
    }
    public static class Scenes
    {
        public static readonly PackedScene SmallLogScene = GD.Load<PackedScene>("res://Scenes/Objects/Trees/SmallLog.tscn");
        public static readonly PackedScene LargeLogScene = GD.Load<PackedScene>("res://Scenes/Objects/Trees/LargeLog.tscn");
    }
}
