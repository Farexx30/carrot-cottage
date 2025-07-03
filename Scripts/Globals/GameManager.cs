using CarrotCottage.Scripts.Characters.PlayerScripts.Inputs;
using CarrotCottage.Scripts.UI.GameMenuScreenScripts;
using Godot;
using System;
using System.Threading.Tasks;

namespace CarrotCottage.Scripts.Globals;

public partial class GameManager : Node
{
    private readonly PackedScene _gameMenuScreenScene = GD.Load<PackedScene>("res://Scenes/UI/GameMenuScreen.tscn");
    private readonly PackedScene _creditsScreenScene = GD.Load<PackedScene>("res://Scenes/UI/CreditsScreen.tscn");


    private SceneManager _sceneManager = default!;
    private SaveGameManager _saveGameManager = default!;

    public bool CanSave { get; private set; } = false;
    public bool GameAlreadyStarted { get; private set; } = true;
    public bool MusicOn { get; private set; } = true;
    public bool SFXOn { get; private set; } = true;

    private bool _shouldLoadLevel = true;

    public override void _Ready()
    {
        _sceneManager = GetNode<SceneManager>(GlobalNames.SceneManager);
        _saveGameManager = GetNode<SaveGameManager>(GlobalNames.SaveGameManager);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed(InputConstants.OpenMainMenu)
            && GetTree().Root.FindChild(nameof(GameMenuScreen), 
                                        recursive: true, 
                                        owned: false) is null)
        {
            OpenMainMenuScreen();
        }
    }

    public async Task StartGame()
    {
        if (!_shouldLoadLevel)
        {
            return;
        }

        _sceneManager.LoadMainSceneContainer();
        await _sceneManager.LoadLevel("Level1");
        await _saveGameManager.LoadGame();

        CanSave = true;
        GameAlreadyStarted = false;
        _shouldLoadLevel = false;
    }

    public void SaveGame()
    {
        _saveGameManager.SaveGame();
    }

    public void GoToCredits()
    {
        OpenCreditsScreen();
    }

    public void ExitGame()
    {
        GetTree().Quit();
    }

    public void ToggleMusic()
    {
        var musicAudioBusIndex = AudioServer.GetBusIndex("Music");

        if (musicAudioBusIndex < 0)
        {
            GD.PrintErr("Music audio bus not found.");
            return;
        }

        MusicOn = !MusicOn;
        AudioServer.SetBusMute(musicAudioBusIndex, !MusicOn);
    }

    public void ToggleSFX()
    {
        var animalsSfxAudioBusIndex = AudioServer.GetBusIndex("AnimalsSFX");
        var activitiesSfxAudioBusIndex = AudioServer.GetBusIndex("ActivitiesSFX");

        if (animalsSfxAudioBusIndex < 0
            || activitiesSfxAudioBusIndex < 0)
        {
            GD.PrintErr("AnimalsSFX or ActivitiesSFX audio bus not found.");
            return;
        }

        SFXOn = !SFXOn;
        AudioServer.SetBusMute(animalsSfxAudioBusIndex, !SFXOn);
        AudioServer.SetBusMute(activitiesSfxAudioBusIndex, !SFXOn);
    }

    private void OpenMainMenuScreen()
    {
        var gameMenuScreen = _gameMenuScreenScene.Instantiate<CanvasLayer>();
        GetTree().Root.AddChild(gameMenuScreen);
    }

    private void OpenCreditsScreen()
    {
        var creditsScreen = _creditsScreenScene.Instantiate<CanvasLayer>();
        GetTree().Root.AddChild(creditsScreen);
    }
}
