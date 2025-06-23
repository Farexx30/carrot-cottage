using Godot;
using System;
using System.Threading.Tasks;

namespace CarrotCottage.Scripts.Globals;

public partial class CarrotCottageDialogueManager : Node
{
    [Signal]
    public delegate void GivePlantSeedsEventHandler();

    [Signal]
    public delegate void FeedTheAnimalsEventHandler();

    public void GiveSeeds()
    {
        EmitSignal(SignalName.GivePlantSeeds);
    }

    public void FeedAnimals()
    {
        EmitSignal(SignalName.FeedTheAnimals);
    }
}
