using CarrotCottage.Scripts.Components;
using Godot;
using System.Threading.Tasks;

namespace CarrotCottage.Scripts.Globals;

public partial class SaveGameManager : Node
{
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("SaveGame"))
        {
            SaveGame();
        }
    }

    public void SaveGame()
    {
        var saveLevelDataComponent = (SaveLevelDataComponent)GetTree().GetFirstNodeInGroup(nameof(SaveLevelDataComponent));

        saveLevelDataComponent?.SaveGame();
    }

    public async Task LoadGame()
    {
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

        var saveLevelDataComponent = (SaveLevelDataComponent)GetTree().GetFirstNodeInGroup(nameof(SaveLevelDataComponent));

        saveLevelDataComponent?.LoadGame();
    }
}
