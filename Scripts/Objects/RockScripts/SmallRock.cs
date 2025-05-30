using CarrotCottage.Scripts.Components;
using CarrotCottage.Scripts.Objects.RockScripts;
using CarrotCottage.Scripts.Objects.TreeScripts;
using Godot;
using System;

namespace CarrotCottage.Scripts.Objects.StoneScripts;

public partial class SmallRock : Sprite2D
{
    private HurtComponent _hurtComponent = default!;
    private HealthComponent _healthComponent = default!;

    private readonly PackedScene _stoneScene = RockConstants.Scenes.StoneScene;

    public override void _Ready()
    {
        _hurtComponent = GetNode<HurtComponent>(ComponentNames.HurtComponent);
        _healthComponent = GetNode<HealthComponent>(ComponentNames.HealthComponent);

        _hurtComponent.Hurt += OnHurt;
        _healthComponent.NoHealthReached += OnNoHealthReached;
    }

    private void OnNoHealthReached()
    {
        CallDeferred(nameof(AddStoneScene));
        GD.Print("No health reached for small rock.");

        QueueFree();
    }

    private void AddStoneScene()
    {
        var stoneInstance = _stoneScene.Instantiate<Node2D>();

        stoneInstance.GlobalPosition = GlobalPosition;

        GetParent().AddChild(stoneInstance);
    }

    private void OnHurt(int damage)
    {
        _healthComponent.ApplyDamage(damage);
    }

    public override void _ExitTree()
    {
        // Unsubscribe from signals to prevent memory leaks although it's not strictly necessary in this case since it should be freed automatically.
        _hurtComponent.Hurt -= OnHurt;
        _healthComponent.NoHealthReached -= OnNoHealthReached;
    }
}
