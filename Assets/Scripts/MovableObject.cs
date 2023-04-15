using System;
using UnityEngine;

/// <summary>
/// Component allowing for input to move this gameObject on the xz plane
/// </summary>
public class MovableObject : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    protected InputProvider InputProvider;

    /// <summary>
    /// Moves this object in the specified direction scaled by moveSpeed. The direction input is not normalized and
    /// is therefore capable of moving the object at a speed besides just moveSpeed. Normalize the direction first if
    /// the object needs to be translated by exactly moveSpeed.
    /// </summary>
    /// <param name="direction">The direction to move.</param>
    public void MoveObject(Vector2 direction)
    {
        // Converts input direction into 3D space
        Vector3 offset = new Vector3(direction.x, 0, direction.y) * moveSpeed;

        transform.position += offset;
    }

    public virtual Vector2 GetDirection()
    {
        return InputProvider.GetDirection();
    }
    
    /// Moves object each physics step based on input provided by the input provider
    private void FixedUpdate()
    {
        MoveObject(GetDirection() * Time.deltaTime);   
    }

    private void Awake()
    {
        InputProvider = GetComponent<InputProvider>();
    }
}