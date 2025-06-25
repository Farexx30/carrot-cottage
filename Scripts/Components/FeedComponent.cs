using Godot;

namespace CarrotCottage.Scripts.Components;

public partial class FeedComponent : Area2D
{
    [Signal]
    public delegate void FoodReceivedEventHandler(Area2D food);

    private void OnAreaEntered(Area2D area)
    {
        EmitSignal(SignalName.FoodReceived, area);
    }
}
