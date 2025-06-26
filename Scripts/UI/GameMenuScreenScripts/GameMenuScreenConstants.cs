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
        public const string StartGameButton = "MarginContainer/VBoxContainer/StartOrContinueGameButton";
        public const string SaveGameButton = "MarginContainer/VBoxContainer/SaveGameButton";
        public const string ExitGameButton = "MarginContainer/VBoxContainer/ExitGameButton";
    }

    public static class Texts
    {
        public const string StartGameButtonText = "Start";
        public const string ContinueGameButtonText = "Continue";
    }
}
