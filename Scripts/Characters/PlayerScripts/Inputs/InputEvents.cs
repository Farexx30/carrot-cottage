using Godot;
using System;
using static Godot.TextServer;

namespace CarrotCottage.Scripts.Characters.PlayerScripts.Inputs;

public static class InputEvents
{
    private static Vector2 s_direction;

    public static Vector2 MovementInputDirection()
    {
        if (Input.IsActionPressed(InputConstants.WalkUp))
        {
            s_direction = Vector2.Up;
        }
        else if (Input.IsActionPressed(InputConstants.WalkDown))
        {
            s_direction = Vector2.Down;
        }
        else if (Input.IsActionPressed(InputConstants.WalkLeft))
        {
            s_direction = Vector2.Left;
        }
        else if (Input.IsActionPressed(InputConstants.WalkRight))
        {
            s_direction = Vector2.Right;
        }
        else
        {
            s_direction = Vector2.Zero;
        }

        return s_direction;
    }

    public static bool IsMovementInput() 
        => s_direction != Vector2.Zero;
}
