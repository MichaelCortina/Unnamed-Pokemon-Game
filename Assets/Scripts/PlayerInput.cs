using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour, InputProvider
{
    [SerializeField] private MovementOption movementType;

    /// provides a vector between 0 and 1 based on player keyboard or controller input
    public Vector2 GetDirection()
    {
        // get keyboard or controller input from user
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // clamp input to 1 axis if on grid, otherwise 
        // maintain raw input
        Vector2 moveDirection;
        if (movementType is MovementOption.Grid)
            moveDirection = ClampAxis(x, y);
        else
            moveDirection = new(x, y);

        return moveDirection;
    }

    /// returns a Vector2 with the greater of x & y favoring x if equal,
    /// the other axis is set to 0
    private static Vector2 ClampAxis(float x, float y)
        => Mathf.Abs(x) < Mathf.Abs(y) ? new(0, y) : new(x, 0);
}

public enum MovementOption
{
    Grid,
    Free
}