using UnityEngine;

public interface InputProvider
{
    /// provides a vector between 0 and 1 based on player keyboard or controller input
    Vector2 GetDirection();
}