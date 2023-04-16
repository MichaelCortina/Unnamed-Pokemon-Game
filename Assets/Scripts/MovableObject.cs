using System;
using UnityEngine;

/// <summary>
/// Component allowing for input to move this gameObject on the xz plane
/// </summary>
public class MovableObject : MonoBehaviour
{
    /// Units per second move speed of this object.
    [SerializeField] private float moveSpeed;
    /// Layers that this GameObject will collide with.
    [SerializeField] private LayerMask movementBlockers;
    
    protected InputProvider InputProvider;
    private Collider _collider;
    private bool _hasCollider;

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

        if (_hasCollider && !_collider.isTrigger)
        {
            // Check x and z collision separately. For each axis, scale the offset that would occur by the distance
            // to the collision so as to have this Object smoothly stop against whatever it is colliding with.
            RaycastHit hit;
            if (IsColliding(Vector3.right * offset.x, offset.x, out hit)) offset.x *= hit.distance;
            if (IsColliding(Vector3.forward * offset.z, offset.z, out hit)) offset.z *= hit.distance;
        }
        transform.position += offset;
    }

    /// <summary>
    /// Performs a box cast with bounds defined by this objects Collider.
    /// </summary>
    /// <param name="direction">Vector3 direction of the cast</param>
    /// <param name="maxDistance">Maximum distance for the cast</param>
    /// <param name="hit">RaycastHit to store collision data in</param>
    /// <returns>True if a collision occured, false if otherwise</returns>
    private bool IsColliding(Vector3 direction, float maxDistance, out RaycastHit hit)
    {
        Bounds bounds = _collider.bounds;
        Transform t = transform;
        Vector3 scale = t.localScale;
        Vector3 extents = bounds.extents;
        // The extents are just a little to big for things to move past each other on the grid, so we scale this by
        // a number just a little bit smaller than 1
        Vector3 scaledExtents = new Vector3(extents.x * scale.x, extents.y * scale.y, extents.z * scale.z) * 0.99f;
        
        bool isColliding = Physics.BoxCast(bounds.center, scaledExtents, direction, out hit,
            Quaternion.identity, Mathf.Abs(maxDistance), movementBlockers);
        return isColliding;
    }

    public virtual Vector2 GetDirection()
    {
        return InputProvider.GetDirection();
    }
    
    /// Moves object each physics step based on input provided by the input provider
    protected void FixedUpdate()
    {
        MoveObject(GetDirection() * Time.deltaTime);   
    }

    private void Awake()
    {
        InputProvider = GetComponent<InputProvider>();

        _collider = GetComponent<Collider>();
        _hasCollider = _collider != null;
    }
}