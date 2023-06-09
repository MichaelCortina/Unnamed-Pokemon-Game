﻿using UnityEngine;

public interface InputProvider
{
    /// Provides a vector between 0 and 1 based on player keyboard or controller input
    Vector2 GetDirection();
}