using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using FixedUpdate = UnityEngine.PlayerLoop.FixedUpdate;

public class GridBoundObject : MovableObject
{
    [SerializeField] private Grid grid;
    private Vector2 _lastDirection = Vector2.zero;

    private void Update()
    {
        UpdateDirection();
    }

    public override Vector2 GetDirection()
    {
        return _lastDirection;
    }
    
    /// <summary>
    /// Update _lastDirection if we have non-zero inputs. Otherwise remember the input until aligned to the grid.
    /// </summary>
    private void UpdateDirection()
    {
        Vector2 direction = InputProvider.GetDirection();
        
        if (direction.x != 0f)
            _lastDirection.x = direction.x;
        else if (IsXAligned())
        {
            _lastDirection.x = 0f;
            SnapX();
        }

        if (direction.y != 0f)
            _lastDirection.y = direction.y;
        else if (IsZAligned())
        {
            _lastDirection.y = 0f;
            SnapZ();
        }
    }

    /// <summary>
    /// Checks whether this object is aligned to its given Grid objects cells.
    /// This only checks in the x and z directions.
    /// </summary>
    /// <returns>True if we are adequately aligned to the grid, false if not</returns>
    private bool IsExactlyOnGrid(float accuracy = 0.1f)
    {
        return IsXAligned(accuracy) && IsZAligned(accuracy);
    }

    /// <summary>
    /// Checks whether this object is aligned with the grid based on the x size of the grid's cells.
    /// </summary>
    /// <param name="accuracy">The minimum distance to the cell position considered to be 'aligned'</param>
    /// <returns>True if our x distance from the grid cell is less than accuracy</returns>
    private bool IsXAligned(float accuracy = 0.1f)
    {
        return Mathf.Abs(transform.position.x % grid.cellSize.x) < accuracy;
    }
    
    /// <summary>
    /// Checks whether this object is aligned with the grid based on the z size of the grid's cells.
    /// </summary>
    /// <param name="accuracy">The minimum distance to the cell position considered to be 'aligned'</param>
    /// <returns>True if our z distance from the grid cell is less than accuracy</returns>
    private bool IsZAligned(float accuracy = 0.1f)
    {
        return Mathf.Abs(transform.position.z % grid.cellSize.z) < accuracy;
    }

    private void SnapToGrid()
    {
        SnapX();
        SnapZ();
    }

    private void SnapX()
    {
        Vector3 pos = transform.position;
        Vector3 size = grid.cellSize;
        float snappedX = pos.x - (pos.x % size.x) + Mathf.Round((pos.x % size.x) / size.x);
        
        transform.position = new Vector3(snappedX, pos.y, pos.z);
    }

    private void SnapZ()
    {
        Vector3 pos = transform.position;
        Vector3 size = grid.cellSize;
        float snappedZ = pos.z - (pos.z % size.z) + Mathf.Round((pos.z % size.z) / size.z);
        
        transform.position = new Vector3(pos.x, pos.y, snappedZ);
    }
}
