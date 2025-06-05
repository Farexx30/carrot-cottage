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

    private int? _startDay;

    public override void _Ready()
    {
        _dayAndNightCycleManager = GetNode<DayAndNightCycleManager>(GlobalNames.DayAndNightCycleManager);

        _dayAndNightCycleManager.TimeTickDay += OnTimeTickDay;
    }

    public override void _ExitTree()
    {
        _dayAndNightCycleManager.TimeTickDay -= OnTimeTickDay;
    }

    private void OnTimeTickDay(int day)
    {
        if (IsWatered)
        {
            _startDay ??= day;

            UpdateGrowthState(_startDay.Value, day);
            UpdateHarvestableState(_startDay.Value, day);
        }      
    }

    private void UpdateGrowthState(int startDay, int currentDay)
    {
        if (CurrentGrowthState == GrowthStates.Mature)
        {
            return;
        }

        var growthDaysPassed = (currentDay - startDay) % GrowthStatesCount;
        var stateIndex = growthDaysPassed % GrowthStatesCount + 1;

        CurrentGrowthState = (GrowthStates)stateIndex;
        GD.Print($"Current Growth State name: {CurrentGrowthState}, Current Growth state index: {stateIndex}.");

        if (CurrentGrowthState == GrowthStates.Mature)
        {
            EmitSignal(SignalName.PlantMature);
        }
    }

    private void UpdateHarvestableState(int startDay, int currentDay)
    {
        if (CurrentGrowthState == GrowthStates.Harvestable)
        {
            return;
        }

        var daysPassed = (currentDay - startDay) % DaysUntilHarvest;

        if (daysPassed == DaysUntilHarvest - 1)
        {
            CurrentGrowthState = GrowthStates.Harvestable;
            EmitSignal(SignalName.PlantHarvestable);
        }
    }
}
