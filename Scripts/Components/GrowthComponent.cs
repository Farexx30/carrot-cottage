using CarrotCottage.Scripts.Common;
using CarrotCottage.Scripts.Globals;
using Godot;
using System;

namespace CarrotCottage.Scripts.Components;

public partial class GrowthComponent : Node
{
    [Export]
    public GrowthStates CurrentGrowthState { get; set; } = GrowthStates.Sprout;

    [Export(PropertyHint.Range, "5,365,1")]
    public int DaysUntilHarvest { get; set; } = 5;

    [Signal]
    public delegate void PlantMatureEventHandler();

    [Signal]
    public delegate void PlantHarvestableEventHandler();

    public bool IsWatered { get; set; } = false;


    private DayAndNightCycleManager _dayAndNightCycleManager = default!;

    private int? _startHour;
    private int? _startMinute;
    private int? _startDay;

    private int? _growthDays;

    public override void _Ready()
    {
        _dayAndNightCycleManager = GetNode<DayAndNightCycleManager>(GlobalNames.DayAndNightCycleManager);

        _dayAndNightCycleManager.TimeTick += OnTimeTick;
    }

    public override void _ExitTree()
    {
        _dayAndNightCycleManager.TimeTick -= OnTimeTick;
    }

    private void OnTimeTick(int day, int hour, int minute)
    {
        if (IsWatered)
        {
            _startDay ??= day;
            _growthDays ??= day;

            if (ShouldUpdateState(day, hour, minute))
            {
                UpdateGrowthState();
                UpdateHarvestableState(currentDay: day);
                
                ++_growthDays;
            }

            _startHour ??= hour;
            _startMinute ??= minute;
        }
    }

    private bool ShouldUpdateState(int day, int hour, int minute)
    {
        return _startHour.HasValue
            && _startMinute.HasValue
            && day > _growthDays
            && ((hour == _startHour.Value && minute >= _startMinute.Value)
                || hour > _startHour.Value);
    }

    private void UpdateGrowthState()
    {
        if (CurrentGrowthState == GrowthStates.Mature)
        {
            return;
        }

        ++CurrentGrowthState;
        GD.Print($"{GetParent().Name} - Current Growth State name: {CurrentGrowthState}.");

        if (CurrentGrowthState == GrowthStates.Mature)
        {
            EmitSignal(SignalName.PlantMature);
        }
    }

    private void UpdateHarvestableState(int currentDay)
    {
        if (CurrentGrowthState == GrowthStates.Harvestable
            || CurrentGrowthState != GrowthStates.Mature)
        {
            return;
        }

        var daysPassed = currentDay - _startDay!.Value;

        if (daysPassed == DaysUntilHarvest)
        {
            CurrentGrowthState = GrowthStates.Harvestable;
            EmitSignal(SignalName.PlantHarvestable);
        }
    }
}
