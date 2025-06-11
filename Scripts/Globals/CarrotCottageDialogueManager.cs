using Godot;
using System;

namespace CarrotCottage.Scripts.Globals;

public partial class CarrotCottageDialogueManager : Node
{
    [Signal]
    public delegate void GivePlantSeedsEventHandler();

    public override void _Ready()
    {
        base._Ready();
    }

    public void GiveSeeds()
    {
        EmitSignal(SignalName.GivePlantSeeds);
    }
}
