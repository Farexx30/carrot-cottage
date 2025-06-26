using Godot;
using System;

namespace CarrotCottage.Scripts.Components;

public partial class MouseCursorComponent : Node
{
    [Export]
    private Texture2D _cursorTexture = default!;

    public override void _Ready()
    {
        Input.SetCustomMouseCursor(image: _cursorTexture, shape: Input.CursorShape.Arrow);
    }
}
