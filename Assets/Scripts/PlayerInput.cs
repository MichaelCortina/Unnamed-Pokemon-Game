using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

/// <summary>
/// A specialized InputProvider used to relay inputs from the user to the player character.
/// </summary>
public class PlayerInput : MonoBehaviour, InputProvider
{
    /// Returns a Vector2 containing the horizontal and vertical components
    /// of the input axis in the x and y components respectively.
    /// The returned Vector2 is normalized, so it's components will not directly
    /// reflect the return value of the respective Input.GetAxisRaw() call.
    public Vector2 GetDirection()
    {
        // Get keyboard or controller input from user
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(x, y);
        return moveDirection.normalized;
    }

    /// returns a Vector2 with the greater of x & y favoring x if equal,
    /// the other axis is set to 0
    private static Vector2 ClampAxis(float x, float y)
        => Mathf.Abs(x) < Mathf.Abs(y) ? new(0, y) : new(x, 0);
}