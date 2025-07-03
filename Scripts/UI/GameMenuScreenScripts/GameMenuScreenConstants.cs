using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrotCottage.Scripts.UI.GameMenuScreenScripts;

public static class GameMenuScreenConstants
{
    public static class Nodes
    {
        public const string StartOrContinueGameButton = "MarginContainer/VBoxContainer/StartOrContinueGameButton";
        public const string SaveGameButton = "MarginContainer/VBoxContainer/SaveGameButton";
        public const string ExitGameButton = "MarginContainer/VBoxContainer/ExitGameButton";
        public const string MusicToggleButton = "MarginContainer/HBoxContainer/MusicHBoxContainer/Button";
        public const string SFXToggleButton = "MarginContainer/HBoxContainer/SFXHBoxContainer/Button";
    }

    public static class Texts
    {
        public const string StartGameButtonText = "Start";
        public const string ContinueGameButtonText = "Continue";
    }
}
