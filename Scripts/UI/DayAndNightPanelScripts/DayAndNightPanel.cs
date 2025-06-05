using CarrotCottage.Scripts.Globals;
using CarrotCottage.Scripts.UI.DayAndNightPanelScripts;
using Godot;

namespace CarrotCottage.Scripts.UI.DayAndNightPanelScripts;

public partial class DayAndNightPanel : Control
{
    private DayAndNightCycleManager _dayAndNightCycleManager = default!;
    private Label _dayCounterLabel = default!;
    private Label _clockLabel = default!;

    [Export]
    public int RegularSpeed { get; set; } = 1;

    [Export]
    public int FastSpeed { get; set; } = 5;

    [Export]
    public int LightningMcQueenSpeed { get; set; } = 200;

    public override void _Ready()
    {
        _dayAndNightCycleManager = GetNode<DayAndNightCycleManager>(GlobalNames.DayAndNightCycleManager);
        _dayCounterLabel = GetNode<Label>(DayAndNightPanelConstants.DayCounterLabel);
        _clockLabel = GetNode<Label>(DayAndNightPanelConstants.ClockLabel);

        _dayAndNightCycleManager.TimeTick += OnTimeTick;
    }

    private void OnTimeTick(int day, int hour, int minute)
    {
        _dayCounterLabel.Text = $"Day {day}";
        _clockLabel.Text = $"{hour:D2}:{minute:D2}";
    }

    public void OnRegularSpeedButtonPressed()
    {
        _dayAndNightCycleManager.GameSpeed = RegularSpeed;
    }

    public void OnFastSpeedButtonPressed()
    {
        _dayAndNightCycleManager.GameSpeed = FastSpeed;
    }

    public void OnLightningMcQueenSpeedButtonPressed()
    {
        _dayAndNightCycleManager.GameSpeed = LightningMcQueenSpeed;
    }
}
