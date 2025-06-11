using Godot;
using System.Collections.Generic;

namespace CarrotCottage.Scripts.UI.EmotesPanelScripts;

public partial class EmotesPanel : Panel
{
    private AnimatedSprite2D _animatedSprite2D = default!;
    private Timer _emoteIdleTimer = default!;

    private readonly string[] _idleEmotes =
    [
        EmotesPanelConstants.Animations.Emote1Idle,
        EmotesPanelConstants.Animations.Emote2Smile,
        EmotesPanelConstants.Animations.Emote3EarWave,
        EmotesPanelConstants.Animations.Emote4Blink
    ];

    public override void _Ready()
    {
        _animatedSprite2D = GetNode<AnimatedSprite2D>(EmotesPanelConstants.Nodes.AnimatedSprite2D);
        _emoteIdleTimer = GetNode<Timer>(EmotesPanelConstants.Nodes.EmoteIdleTimer);

        _animatedSprite2D.Play(EmotesPanelConstants.Animations.Emote1Idle);
    }

    public void OnEmoteIdleTimerTimeout()
    {
        var index = GD.RandRange(0, _idleEmotes.Length - 1);
        var emoteName = _idleEmotes[index];

        _animatedSprite2D.Play(emoteName);
    }
}
