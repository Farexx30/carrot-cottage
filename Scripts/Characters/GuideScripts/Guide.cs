using CarrotCottage.Scripts.Characters.PlayerScripts.Inputs;
using CarrotCottage.Scripts.Components;
using CarrotCottage.Scripts.Dialogues;
using DialogueManagerRuntime;
using Godot;
using System;

namespace CarrotCottage.Scripts.Characters.GuideScripts;

public partial class Guide : Node2D
{
    private readonly PackedScene _dialogueBalloonScene = GD.Load<PackedScene>("res://Scenes/Dialogues/DialogueBalloon.tscn");

    private InteractableComponent _interactableComponent = default!;
    private Control _interactableLabelComponent = default!;

    private bool _isInDialogueRange = false;

    public override void _Ready()
    {
        _interactableComponent = GetNode<InteractableComponent>(GuideConstants.Nodes.InteractableComponent);
        _interactableLabelComponent = GetNode<Control>(GuideConstants.Nodes.InteractableLabelComponent);

        _interactableComponent.InteractableActivated += OnInteractableActivated;
        _interactableComponent.InteractableDeactivated += OnInteractableDeactivated;

        _interactableLabelComponent.Visible = false;
    }

    private void OnInteractableActivated()
    {
        _interactableLabelComponent.Visible = true;
        _isInDialogueRange = true;
    }

    private void OnInteractableDeactivated()
    {
        _interactableLabelComponent.Visible = false;
        _isInDialogueRange = false;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed(InputConstants.ShowDialogue)
            && _isInDialogueRange)
        {
            var dialogueBalloonInstance = _dialogueBalloonScene.Instantiate<BaseDialogueBalloon>();
            GetTree().CurrentScene.AddChild(dialogueBalloonInstance);
            dialogueBalloonInstance.Start(GD.Load<Resource>("res://Dialogues/GuideDialogue.dialogue"), "start");
        }
    }
}
