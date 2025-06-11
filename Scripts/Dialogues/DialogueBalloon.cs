using CarrotCottage.Scripts.UI.EmotesPanelScripts;
using DialogueManagerRuntime;
using Godot;
using Godot.Collections;
using System;

namespace CarrotCottage.Scripts.Dialogues;

public partial class DialogueBalloon : BaseDialogueBalloon
{
    private EmotesPanel _emotesPanel = default!;

    public override void _Ready()
    {
        base._Ready();

        _emotesPanel = GetNode<EmotesPanel>(EmotesPanelConstants.Path);
    }

    public override void Start(Resource dialogueResource, string title, Array<Variant>? extraGameStates = null)
    {
        base.Start(dialogueResource, title, extraGameStates);

        _emotesPanel.PlayEmote(EmotesPanelConstants.Animations.Talking);
    }

    public override void Next(string nextId)
    {
        base.Next(nextId);

        _emotesPanel.PlayEmote(EmotesPanelConstants.Animations.Talking);
    }
}
