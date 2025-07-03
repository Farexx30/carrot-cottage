using Godot;
using System;

namespace CarrotCottage.Scripts.UI.CreditsScreenScripts;

public partial class CreditsScreen : CanvasLayer
{
    private void OnExitButtonPressed()
    {
        QueueFree();
    }
}
