using CarrotCottage.Scripts.Globals;
using Godot;
using System;

namespace CarrotCottage.Scripts.Components;

public partial class DayAndNightCycleComponent : CanvasModulate
{
    [Export]
    public int InitialDay { get; set; } = 1;

    [Export]
    public int InitialHour { get; set; } = 6;

    [Export]
    public int InitialMinute { get; set; } = 0;

    [Export]
    public GradientTexture1D DayAndNightGradientTexture { get; set; } = default!;


    private DayAndNightCycleManager _dayAndNightCycleManager = default!; 

    public override void _Ready()
    {
        _dayAndNightCycleManager = GetNode<DayAndNightCycleManager>(GlobalNames.DayAndNightCycleManager);

        _dayAndNightCycleManager.InitialDay = InitialDay;
        _dayAndNightCycleManager.InitialHour = InitialHour;
        _dayAndNightCycleManager.InitialMinute = InitialMinute;
        _dayAndNightCycleManager.SetInitialTime();

        _dayAndNightCycleManager.GameTime += OnGameTime;
    }

    private void OnGameTime(float time)
    {
        var sampleValue = (float)((Math.Sin(time - Math.PI / 2) + 1.0) / 2) ;
        Color = DayAndNightGradientTexture.Gradient.Sample(sampleValue);
    }
}
