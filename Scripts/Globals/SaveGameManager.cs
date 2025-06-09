using CarrotCottage.Scripts.Components;
using Godot;

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



    private void SaveGame()
    {
        var saveLevelDataComponent = (SaveLevelDataComponent)GetTree().GetFirstNodeInGroup(nameof(SaveLevelDataComponent));

        saveLevelDataComponent?.SaveGame();
    }
    public void LoadGame()
    {
        var saveLevelDataComponent = (SaveLevelDataComponent)GetTree().GetFirstNodeInGroup(nameof(SaveLevelDataComponent));

        saveLevelDataComponent?.LoadGame();
    }
}
