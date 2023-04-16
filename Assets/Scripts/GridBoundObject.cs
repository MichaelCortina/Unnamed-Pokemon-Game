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

    protected new void FixedUpdate()
    {
        UpdateDirection();
        Vector3 lastPos = transform.position;
        base.FixedUpdate();
        Vector3 posChange = transform.position - lastPos;
        // TODO: Make this SnapToGrid and zero _lastDirection if the position change is small enough, not just if it is zero
        if (posChange == Vector3.zero)
        {
            SnapToGrid();
            _lastDirection = Vector3.zero;
        }
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
        float xPos = transform.position.x;
        float xSize = grid.cellSize.x;
        bool aligned = Mathf.Abs(xPos % xSize) < accuracy;

        return aligned;
    }
    
    /// <summary>
    /// Checks whether this object is aligned with the grid based on the z size of the grid's cells.
    /// </summary>
    /// <param name="accuracy">The minimum distance to the cell position considered to be 'aligned'</param>
    /// <returns>True if our z distance from the grid cell is less than accuracy</returns>
    private bool IsZAligned(float accuracy = 0.1f)
    {
        float zPos = transform.position.z;
        float zSize = grid.cellSize.z;
        bool aligned = Mathf.Abs(zPos % zSize) < accuracy;
            
        return aligned;
    }

    private void SnapToGrid()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(GetSnappedValue(pos.x, grid.cellSize.x), pos.y, GetSnappedValue(pos.z, grid.cellSize.z));
    }

    private void SnapX()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(GetSnappedValue(pos.x, grid.cellSize.x), pos.y, pos.z);
    }
    
    private void SnapZ()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y, GetSnappedValue(pos.z, grid.cellSize.z));
    }

    private float GetSnappedValue(float position, float gridSize)
    {
        float localPosInCell = position % gridSize;
        float flooredCellPos = position - localPosInCell;
        float percentPosInCell = localPosInCell / gridSize;
        return flooredCellPos + (Mathf.Round(percentPosInCell) * gridSize);
    }
}
