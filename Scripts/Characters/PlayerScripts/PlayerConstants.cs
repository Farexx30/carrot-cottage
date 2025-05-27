using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrotCottage.Scripts.Characters.PlayerScripts;

public static class PlayerConstants
{
    public static class Animations
    {
        public const string IdleFront = "IdleFront";
        public const string IdleBack = "IdleBack";
        public const string IdleLeft = "IdleLeft";
        public const string IdleRight = "IdleRight";

        public const string WalkFront = "WalkFront";
        public const string WalkBack = "WalkBack";
        public const string WalkLeft = "WalkLeft";
        public const string WalkRight = "WalkRight";
    }

    public static class States
    {
        public const string Idle = "Idle";
        public const string Walk = "Walk";
    }
}
