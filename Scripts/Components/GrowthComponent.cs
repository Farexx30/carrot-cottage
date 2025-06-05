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


    private const int GrowthStatesCount = 5; // Exclude 'Harvestable' state

    private DayAndNightCycleManager _dayAndNightCycleManager = default!;

    private int? _startHour;
    private int? _startMinute;
    private int? _startDay;

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

            if (ShouldUpdateState(day, hour, minute))
            {
                UpdateGrowthState(currentDay: day);
                UpdateHarvestableState(currentDay: day);
            }

            _startHour ??= hour;
            _startMinute ??= minute;
        }
    }

    private bool ShouldUpdateState(int day, int hour, int minute)
    {
        return _startHour.HasValue
            && _startMinute.HasValue
            && day > _startDay!.Value 
            && ((hour == _startHour.Value && minute >= _startMinute.Value)               
                || hour > _startHour.Value)
            && (hour <= _startHour.Value + 1); //Added
    }

    private void UpdateGrowthState(int currentDay)
    {
        if (CurrentGrowthState == GrowthStates.Mature)
        {
            return;
        }

        var stateIndex = (currentDay - _startDay!.Value) % GrowthStatesCount;

        CurrentGrowthState = (GrowthStates)stateIndex;
        GD.Print($"{GetParent().Name} - Current Growth State name: {CurrentGrowthState}, Current Growth state index: {stateIndex}.");

        if (CurrentGrowthState == GrowthStates.Mature)
        {
            EmitSignal(SignalName.PlantMature);
        }
    }

    private void UpdateHarvestableState(int currentDay)
    {
        if (CurrentGrowthState == GrowthStates.Harvestable)
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
