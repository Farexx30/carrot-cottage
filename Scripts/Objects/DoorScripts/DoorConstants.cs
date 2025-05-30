using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrotCottage.Scripts.Objects.DoorScripts;

public static class DoorConstants
{
    public static class Nodes
    {
        public const string AnimatedSprite2D = "AnimatedSprite2D";
        public const string CollisionShape2D = "CollisionShape2D";
    }

    public static class Animations
    {
        public const string Idle = "Idle";
        public const string OpeningOrClosing = "OpeningOrClosing";
    }
}
