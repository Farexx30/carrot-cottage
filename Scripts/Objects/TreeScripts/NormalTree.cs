using CarrotCottage.Scripts.Components;
using CarrotCottage.Scripts.Objects.TreeScripts;
using Godot;
using System;

namespace CarrotCottage.Scripts.Objects.TreeScripts;

public partial class NormalTree : Sprite2D
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
        CallDeferred(nameof(AddLogScenes));
        GD.Print("No health reached for normal tree.");
        QueueFree();
    }

    private void AddLogScenes()
    {
        var smallLogInstance1 = _smallLogScene.Instantiate<Node2D>();
        var smallLogInstance2 = _smallLogScene.Instantiate<Node2D>();

        var logPosition = GlobalPosition;
        logPosition.Y += 8;
        smallLogInstance1.GlobalPosition = logPosition;
        logPosition.X += 2;
        logPosition.Y += 6;
        smallLogInstance2.GlobalPosition = logPosition;

        GetParent().AddChild(smallLogInstance1);
        GetParent().AddChild(smallLogInstance2);
    }

    private void OnHurt(int damage)
    {
        _healthComponent.ApplyDamage(damage);
    }
}
