using CarrotCottage.Scripts.Components;
using CarrotCottage.Scripts.Objects.TreeScripts;
using Godot;
using System;

namespace CarrotCottage.Scripts.Objects.TreeScripts;

public partial class LargeTree : Sprite2D
{
    private HurtComponent _hurtComponent = default!;
    private HealthComponent _healthComponent = default!;
    private AudioStreamPlayer2D _hitSFXAudioPlayer = default!;

    private readonly PackedScene _smallLogScene = TreeConstants.Scenes.SmallLogScene;
    private readonly PackedScene _largeLogScene = TreeConstants.Scenes.LargeLogScene;

    public override void _Ready()
    {
        _hurtComponent = GetNode<HurtComponent>(ComponentNames.HurtComponent);
        _healthComponent = GetNode<HealthComponent>(ComponentNames.HealthComponent);
        _hitSFXAudioPlayer = GetNode<AudioStreamPlayer2D>(TreeConstants.Nodes.HitSFX);

        _hurtComponent.Hurt += OnHurt;
        _healthComponent.NoHealthReached += OnNoHealthReached;
    }

    private void OnNoHealthReached()
    {
        CallDeferred(nameof(AddLogScenes));
        GD.Print("No health reached for large tree.");
        QueueFree();
    }

    private void AddLogScenes()
    {
        var smallLogInstance1 = _smallLogScene.Instantiate<Node2D>();
        var smallLogInstance2 = _smallLogScene.Instantiate<Node2D>();
        var largeLogInstance = _largeLogScene.Instantiate<Node2D>();

        var logPosition = GlobalPosition;
        logPosition.Y += 8;
        smallLogInstance1.GlobalPosition = logPosition;

        logPosition.X += 2;
        logPosition.Y += 6;
        smallLogInstance2.GlobalPosition = logPosition;

        logPosition.X -= 7;
        logPosition.Y -= 4;
        largeLogInstance.GlobalPosition = logPosition;

        GetParent().AddChild(largeLogInstance);
        GetParent().AddChild(smallLogInstance1);
        GetParent().AddChild(smallLogInstance2);
    }

    private async void OnHurt(int damage)
    {
        _hitSFXAudioPlayer.Play();
        _healthComponent.ApplyDamage(damage);

        if (Material is ShaderMaterial shaderMaterial)
        {
            shaderMaterial.SetShaderParameter("shake_intensity", 0.8f);
            await ToSignal(GetTree().CreateTimer(0.5f), Timer.SignalName.Timeout);
            shaderMaterial.SetShaderParameter("shake_intensity", 0.0f);
        }
    }

    public override void _ExitTree()
    {
        // Unsubscribe from signals to prevent memory leaks although it's not strictly necessary in this case since it should be freed automatically.
        _hurtComponent.Hurt -= OnHurt;
        _healthComponent.NoHealthReached -= OnNoHealthReached;
    }
}
