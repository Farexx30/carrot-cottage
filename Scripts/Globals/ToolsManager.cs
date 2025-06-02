using CarrotCottage.Scripts.Characters.PlayerScripts;
using Godot;

namespace CarrotCottage.Scripts.Globals;

public partial class ToolsManager : Node
{
    public PlayerTools CurrentTool { get; private set; } = PlayerTools.None;

    [Signal]
    public delegate void ToolChangedEventHandler(PlayerTools newTool);

    public void ChangeTool(PlayerTools tool)
    {
        if (CurrentTool == tool)
        {
            return;
        }

        EmitSignal(SignalName.ToolChanged, (int)tool);
        CurrentTool = tool;
    }
}
