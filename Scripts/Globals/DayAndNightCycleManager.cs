using Godot;
using System;

namespace CarrotCottage.Scripts.Globals;

public partial class DayAndNightCycleManager : Node
{
    private const int MinutesPerDay = 24 * 60; 
    private const int MinutesPerHour = 60;
    private const float GameMinuteDuration = (float)Math.Tau / MinutesPerDay;

    public float GameSpeed { get; set; } = 1.0f;
    public int InitialDay { get; set; } = 1;

    public int InitialHour { get; set; } = 6; 
    public int InitialMinute { get; set; } = 0;

    private float _time = 0.0f;
    private int _currentDay = 0;
    private int _currentMinute = -1;

    [Signal]
    public delegate void GameTimeEventHandler(float time);

    [Signal]
    public delegate void TimeTickEventHandler(int day, int hour, int minute);

    [Signal]
    public delegate void TimeTickDayEventHandler(int day);

    public override void _Ready()
    {
        SetInitialTime();
    }

    public override void _Process(double delta)
    {
        _time += (float)delta * GameSpeed * GameMinuteDuration;
        EmitSignal(SignalName.GameTime, _time);

        UpdateTime();
    }

    public void SetInitialTime()
    {
        var initialTotalMinutes = InitialDay * MinutesPerDay + InitialHour * MinutesPerHour + InitialMinute;
    
        _time = initialTotalMinutes * GameMinuteDuration;
    }

    private void UpdateTime()
    {
        int totalMinutes = (int)(_time / GameMinuteDuration);
        int day = totalMinutes / MinutesPerDay;
        var currentDayMinutes = totalMinutes % MinutesPerDay;
        int hour = currentDayMinutes / MinutesPerHour;
        int minute = currentDayMinutes % MinutesPerHour;

        if (_currentMinute != minute)
        {
            _currentMinute = minute;
            EmitSignal(SignalName.TimeTick, day, hour, minute);
        }

        if (_currentDay != day)
        {
            _currentDay = day;
            EmitSignal(SignalName.TimeTickDay, day);
        }
    }

}
