using Godot;
using Godot.Collections;
using System.Threading.Tasks;

namespace CarrotCottage.Scripts.Globals;

public partial class SceneManager : Node
{
    private readonly StringName _mainScenePath = "res://Scenes/MainScene.tscn";
    private readonly NodePath _mainSceneLevelRootPath = "/root/MainScene/GameRoot/LevelRoot";
    private readonly NodePath _mainSceneRootPath = "/root/MainScene";

    private readonly Dictionary<StringName, StringName> _levelScenes = new()
    {
        { "Level1", "res://Scenes/Levels/Level1.tscn" }
    };

    public void LoadMainSceneContainer()
    {
        if (GetTree().Root.HasNode(_mainSceneRootPath))
        {
            return; // Main scene is already loaded
        }

        var node = GD.Load<PackedScene>(_mainScenePath).Instantiate();

        if (node is not null)
        {
            GetTree().Root.AddChild(node);
        }
    }

    public async Task LoadLevel(StringName levelName)
    {
        if (!_levelScenes.TryGetValue(levelName, out var levelScenePath))
        {
            return;
        }

        var levelScene = GD.Load<PackedScene>(levelScenePath).Instantiate();
        var levelRoot = GetNodeOrNull<Node>(_mainSceneLevelRootPath);

        if (levelRoot is null)
        {
            return;          
        }

        var nodes = levelRoot.GetChildren();

        if (nodes is null)
        {
            return;
        }
        
        foreach (var node in nodes)
        {
            node.QueueFree();
        }

        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

        levelRoot.AddChild(levelScene);

        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
    }
}
