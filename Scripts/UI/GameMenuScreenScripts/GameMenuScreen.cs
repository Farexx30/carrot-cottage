using CarrotCottage.Scripts.Globals;
using Godot;

namespace CarrotCottage.Scripts.UI.GameMenuScreenScripts;

public partial class GameMenuScreen : CanvasLayer
{
    private GameManager _gameManager = default!;

    public override void _Ready()
    {
        _gameManager = GetNode<GameManager>(GlobalNames.GameManager);
    }

    private void OnStartGameButtonPressed()
    {
        _gameManager.StartGame();
        QueueFree();
    }

    private void OnSaveGameButtonPressed()
    {

    }

    private void OnExitGameButtonPressed()
    {

    }

}
