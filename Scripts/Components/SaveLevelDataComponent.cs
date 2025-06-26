using CarrotCottage.Scripts.Resources;
using Godot;
using System.Linq;

namespace CarrotCottage.Scripts.Components;

public partial class SaveLevelDataComponent : Node
{
    [Export]
    public StringName LevelSceneName { get; set; } = default!;

    private StringName _saveGameDataPath = "user://GameData/";
    private SaveGameDataResource _gameDataResource = default!;

    public override void _Ready()
    {
        AddToGroup(nameof(SaveLevelDataComponent));
    }

    private void SaveNodeData()
    { 
        var nodes = GetTree().GetNodesInGroup(nameof(SaveDataComponent));

        _gameDataResource = new SaveGameDataResource();

        if (nodes is not null)
        {
            foreach (var node in nodes)
            {
                if (node is SaveDataComponent saveDataComponent)
                {
                    var saveDataResource = saveDataComponent.SaveData();
                    var saveFinalResource = (NodeDataResource)saveDataResource!.Duplicate();
                    _gameDataResource.SaveDataNodes.Add(saveFinalResource);
                }
            }
        }
    }

    public void SaveGame()
    { 
        if (!DirAccess.DirExistsAbsolute(_saveGameDataPath))
        {
            DirAccess.MakeDirAbsolute(_saveGameDataPath);    
        }

        var levelSaveFileName = $"Save{LevelSceneName}GameData.tres";

        SaveNodeData();

        var result = ResourceSaver.Save(
            resource: _gameDataResource,
            path: $"{_saveGameDataPath}{levelSaveFileName}");

        GD.Print($"Save result: {result}");

    }

    public void LoadGame()
    {
        string levelSaveFileName = $"Save{LevelSceneName}GameData.tres";
        string saveGamePath = $"{_saveGameDataPath}{levelSaveFileName}";

        if (!FileAccess.FileExists(saveGamePath))
        {
            // GD.PrintErr($"Save file does not exist at path: {saveGamePath}");
            return;
        }

        _gameDataResource = GD.Load<SaveGameDataResource>(saveGamePath);

        if (_gameDataResource is null)
        {
            GD.PrintErr("Failed to load game data resource.");
            return;
        }

        var rootNode = GetTree().Root;

        foreach (var nodeData in _gameDataResource.SaveDataNodes)
        {
            nodeData.LoadData(rootNode);
        }
    }
}
