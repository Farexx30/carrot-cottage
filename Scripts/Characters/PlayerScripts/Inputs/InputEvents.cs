using Godot;
using System;
using static Godot.TextServer;

namespace CarrotCottage.Scripts.Characters.PlayerScripts.Inputs;

public static class InputEvents
{
    public static bool CanMove { get; set; } = true;

    private static Vector2 s_direction;

    public static Vector2 MovementInputDirection()
    {
        if (!CanMove)
        {
            s_direction = Vector2.Zero;
            return s_direction;
        }

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

    public static bool IsHitInput()
        => Input.IsActionJustPressed(InputConstants.Hit);
}
