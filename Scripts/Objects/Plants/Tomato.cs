using CarrotCottage.Scripts.Common;
using CarrotCottage.Scripts.Components;
using Godot;
using System;

namespace CarrotCottage.Scripts.Objects.Plants;

public partial class Tomato : Node2D
{
    private const int FrameShift = 20;

    private readonly PackedScene _harvestScene = GD.Load<PackedScene>("res://Scenes/Objects/Plants/TomatoHarvest.tscn");

    private Sprite2D _sprite2D = default!;
    private GpuParticles2D _wateringParticlesComponent = default!;
    private GpuParticles2D _floweringParticlesComponent = default!;
    private GrowthComponent _growthComponent = default!;
    private HurtComponent _hurtComponent = default!;

    private GrowthStates _currentGrowthState = GrowthStates.Seed;

    public override void _Ready()
    {
        _sprite2D = GetNode<Sprite2D>(PlantConstants.Nodes.Sprite2D);
        _wateringParticlesComponent = GetNode<GpuParticles2D>(PlantConstants.Nodes.WateringParticlesComponent);
        _floweringParticlesComponent = GetNode<GpuParticles2D>(PlantConstants.Nodes.FloweringParticlesComponent);
        _growthComponent = GetNode<GrowthComponent>(PlantConstants.Nodes.GrowthComponent);
        _hurtComponent = GetNode<HurtComponent>(PlantConstants.Nodes.HurtComponent);

        _wateringParticlesComponent.Emitting = false;
        _floweringParticlesComponent.Emitting = false;

        _growthComponent.PlantMature += OnPlantMature;
        _growthComponent.PlantHarvestable += OnPlantHarvestable;

        _hurtComponent.Hurt += OnHurt;
    }

    public override void _Process(double delta)
    {
        _sprite2D.Frame = (int)_growthComponent.CurrentGrowthState + FrameShift;
    }

    public override void _ExitTree()
    {
        _growthComponent.PlantMature -= OnPlantMature;
        _growthComponent.PlantHarvestable -= OnPlantHarvestable;
        _hurtComponent.Hurt -= OnHurt;
    }

    private void OnPlantMature()
    {
        _floweringParticlesComponent.Emitting = true;
    }

    private void OnPlantHarvestable()
    {
        var tomatoHarvestInstance = _harvestScene.Instantiate<Sprite2D>();
        tomatoHarvestInstance.GlobalPosition = GlobalPosition;
        GetParent().AddChild(tomatoHarvestInstance);

        QueueFree();
    }

    private async void OnHurt(int _)
    {
        if (!_growthComponent.IsWatered)
        {
            _wateringParticlesComponent.Emitting = true;
            await ToSignal(GetTree().CreateTimer(1.0f), Timer.SignalName.Timeout);
            _wateringParticlesComponent.Emitting = false;

            _growthComponent.IsWatered = true;
        }
    }
}
