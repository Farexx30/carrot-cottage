using CarrotCottage.Scripts.Globals;
using Godot;

namespace CarrotCottage.Scripts.Components;

public partial class TestSceneSaveDataManagerComponent : Node
{
    private SaveGameManager _saveGameManager = default!;
    public override void _Ready()
    {
        _saveGameManager = GetNode<SaveGameManager>(GlobalNames.SaveGameManager);

        CallDeferred(nameof(LoadTestScene));
    }

    private async void LoadTestScene()
    {
        await _saveGameManager.LoadGame();
    }
}
