using Godot;

namespace CarrotCottage.Scripts.Globals;

public partial class GameManager : Node
{
    private SceneManager _sceneManager = default!;
    public override void _Ready()
    {
        _sceneManager = GetNode<SceneManager>(GlobalNames.SceneManager);
    }

    public void StartGame()
    {
        _sceneManager.LoadMainSceneContainer();
        _sceneManager.LoadLevel("Level1");
    }

    public void ExitGame()
    {
        GetTree().Quit();
    }
}
