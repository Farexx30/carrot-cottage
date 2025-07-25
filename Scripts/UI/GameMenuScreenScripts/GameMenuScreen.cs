using CarrotCottage.Scripts.Globals;
using Godot;
using System.Threading.Tasks;
using static Godot.Control;

namespace CarrotCottage.Scripts.UI.GameMenuScreenScripts;

public partial class GameMenuScreen : CanvasLayer
{
    private GameManager _gameManager = default!;

    public override void _Ready()
    {
        _gameManager = GetNode<GameManager>(GlobalNames.GameManager);

        var startOrContinueGameButton = GetNode<Button>(GameMenuScreenConstants.Nodes.StartOrContinueGameButton);
        startOrContinueGameButton.Text = _gameManager.GameAlreadyStarted 
            ? GameMenuScreenConstants.Texts.StartGameButtonText
            : GameMenuScreenConstants.Texts.ContinueGameButtonText;

        var saveGameButton = GetNode<Button>(GameMenuScreenConstants.Nodes.SaveGameButton);
        var canSave = _gameManager.CanSave;
        saveGameButton.Disabled = !canSave;
        saveGameButton.FocusMode = !canSave
            ? FocusModeEnum.None 
            : FocusModeEnum.All;

        var musicToggleButton = GetNode<Button>(GameMenuScreenConstants.Nodes.MusicToggleButton);
        var sfxToggleButton = GetNode<Button>(GameMenuScreenConstants.Nodes.SFXToggleButton);
        musicToggleButton.ButtonPressed = _gameManager.MusicOn;
        sfxToggleButton.ButtonPressed = _gameManager.SFXOn;
    }

    private async void OnStartGameButtonPressed()
    {
        await _gameManager.StartGame();
        QueueFree();
    }

    private void OnSaveGameButtonPressed()
    {
        _gameManager.SaveGame();
    }

    private void OnCreditsButtonPressed()
    {
        _gameManager.GoToCredits();
    }

    private void OnExitGameButtonPressed()
    {
        _gameManager.ExitGame();
    }

    private void OnMusicButtonPressed()
    {
        _gameManager.ToggleMusic();
    }

    private void OnSFXButtonPressed()
    {
        _gameManager.ToggleSFX();
    }
}
