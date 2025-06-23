using CarrotCottage.Scripts.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrotCottage.Scripts.Objects.ChestScripts;

public static class ChestConstants
{
    public static class Nodes
    {
        public const string InteractableComponent = "InteractableComponent";
        public const string InteractableLabelComponent = "InteractableLabelComponent";
        public const string FeedComponent = "FeedComponent";
        public const string AnimatedSprite2D = "AnimatedSprite2D";
        public const string RewardMarker2D = "RewardMarker2D";
    }

    public static class Animations
    {
        public const string Idle = "Idle";
        public const string OpeningOrClosing = "OpeningOrClosing";
    }
}
