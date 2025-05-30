using CarrotCottage.Scripts.Components;
using CarrotCottage.Scripts.Objects.TreeScripts;
using Godot;
using System;

namespace CarrotCottage.Scripts.Objects.TreeScripts;

public partial class SmallTree : Sprite2D
{
    private HurtComponent _hurtComponent = default!;
    private HealthComponent _healthComponent = default!;

    private readonly PackedScene _smallLogScene = TreeConstants.Scenes.SmallLogScene;
    
    public override void _Ready()
    {
        _hurtComponent = GetNode<HurtComponent>(TreeConstants.Nodes.HurtComponent);
        _healthComponent = GetNode<HealthComponent>(TreeConstants.Nodes.HealthComponent);

        _hurtComponent.Hurt += OnHurt;
        _healthComponent.NoHealthReached += OnNoHealthReached;
    }

    private void OnNoHealthReached()
    {
        CallDeferred(nameof(AddLogScene));
        GD.Print("No health reached for small tree.");
        QueueFree();
    }

    private void AddLogScene()
    {
        var smallLogInstance = _smallLogScene.Instantiate<Node2D>();

        var logPosition = GlobalPosition;
        logPosition.Y += 8;
        smallLogInstance.GlobalPosition = logPosition;

        GetParent().AddChild(smallLogInstance);
    }

    private void OnHurt(int damage)
    {
        _healthComponent.ApplyDamage(damage);
    }
}
