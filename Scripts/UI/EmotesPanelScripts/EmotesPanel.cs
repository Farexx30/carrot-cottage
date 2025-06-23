using CarrotCottage.Scripts.Globals;
using Godot;
using System;
using System.Collections.Generic;

namespace CarrotCottage.Scripts.UI.EmotesPanelScripts;

public partial class EmotesPanel : Panel
{
    private InventoryManager _inventoryManager = default!;
    private CarrotCottageDialogueManager _dialogueManager = default!;

    private AnimatedSprite2D _animatedSprite2D = default!;
    private Timer _emoteIdleTimer = default!;

    private readonly StringName[] _idleEmotes =
    [
        EmotesPanelConstants.Animations.Idle,
        EmotesPanelConstants.Animations.Smile,
        EmotesPanelConstants.Animations.EarWave,
        EmotesPanelConstants.Animations.Blink
    ];

    public override void _Ready()
    {
        _inventoryManager = GetNode<InventoryManager>(GlobalNames.InventoryManager);
        _dialogueManager = GetNode<CarrotCottageDialogueManager>(GlobalNames.DialogueManager);
        _animatedSprite2D = GetNode<AnimatedSprite2D>(EmotesPanelConstants.Nodes.AnimatedSprite2D);
        _emoteIdleTimer = GetNode<Timer>(EmotesPanelConstants.Nodes.EmoteIdleTimer);

        _inventoryManager.InventoryChanged += OnInventoryChanged;
        _dialogueManager.FeedTheAnimals += OnFeedTheAnimals;

        _animatedSprite2D.Play(EmotesPanelConstants.Animations.Idle);
    }

    private void OnFeedTheAnimals()
    {

        PlayEmote(EmotesPanelConstants.Animations.Kiss);
    }

    private void OnInventoryChanged(StringName collectableKey)
    {
        PlayEmote(EmotesPanelConstants.Animations.Excited);
    }

    public void PlayEmote(StringName emoteName)
    {
        if (_animatedSprite2D.Animation == emoteName)
        {
            return;
        }

        _animatedSprite2D.Play(emoteName);
    }

    public void OnEmoteIdleTimerTimeout()
    {
        var index = GD.RandRange(0, _idleEmotes.Length - 1);
        var emoteName = _idleEmotes[index];

        PlayEmote(emoteName);
    }

    public override void _ExitTree()
    {
        _inventoryManager.InventoryChanged -= OnInventoryChanged;
        _dialogueManager.FeedTheAnimals -= OnFeedTheAnimals;
    }
}
