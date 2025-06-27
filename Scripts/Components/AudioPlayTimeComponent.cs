using Godot;
using System;

namespace CarrotCottage.Scripts.Components;

public partial class AudioPlayTimeComponent : Timer
{
    [Export]
    public AudioStreamPlayer2D AudioStreamPlayer2D { get; set; } = default!;

    [Export]
    public float MinWaitTime { get; set; } = 10.0f;

    [Export]
    public float MaxWaitTime { get; set; } = 20.0f;

    public override void _Ready()
    {
        WaitTime = GD.RandRange(MinWaitTime, MaxWaitTime);
        Start();
    }

    private void OnTimeout()
    {
        AudioStreamPlayer2D.Play();

        WaitTime = GD.RandRange(MinWaitTime, MaxWaitTime);
        Start();
    }
}
