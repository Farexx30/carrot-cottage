using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrotCottage.Scripts.Objects.RockScripts;

public static class RockConstants
{
    public static class Nodes
    {
        public const string HitSFX = "PickaxeHitRockSfx";
    }

    public static class Scenes
    {
        public static readonly PackedScene StoneScene = GD.Load<PackedScene>("res://Scenes/Objects/Rocks/Stone.tscn");
    }
}
