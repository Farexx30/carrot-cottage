using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrotCottage.Scripts.UI.EmotesPanelScripts;

public static class EmotesPanelConstants
{
    public const string Path = "Balloon/MarginContainer/PanelContainer/MarginContainer/HBoxContainer/EmotesPanel";

    public static class Nodes
    {         
        public const string AnimatedSprite2D = "Emote/AnimatedSprite2D";
        public const string EmoteIdleTimer = "EmoteIdleTimer";
    }

    public static class Animations
    {
        public const string Idle = "Idle";
        public const string Smile = "Smile";
        public const string EarWave = "EarWave";
        public const string Blink = "Blink";
        public const string Talking = "Talking";
        public const string Excited = "Excited";
        public const string Kiss = "Kiss";
    }
}
