using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component allowing for animation of a MovableObject
/// </summary>
[RequireComponent(typeof(MovableObject))]
public class MovableAnimator : MonoBehaviour
{
    private MovableObject _movableObject;
    [SerializeField] private Animator animator;
    [SerializeField] private bool updateGameObjectRotation = false;
    
    private void Awake()
    {
        _movableObject = GetComponent<MovableObject>();
    }

    private void Update()
    {
        Vector2 direction = _movableObject.GetDirection();
        bool isMoving = direction != Vector2.zero;
        UpdateAnimatorParameters(direction);
        if (isMoving && updateGameObjectRotation) UpdateRotation(direction);
    }

    private void UpdateAnimatorParameters(Vector2 direction)
    {
        animator.SetFloat("VelocityX", direction.x);
        animator.SetFloat("VelocityZ", direction.y);
        animator.SetFloat("VelocityMagnitude", direction.magnitude);
    }

    /// <summary>
    /// Update the y-rotation of the object to point in the direction of the specified direction vector. This only
    /// sets the local rotation and may yield unexpected results when in combination with other rotation logic.
    /// </summary>
    /// <param name="direction">A vector pointing in the desired direction</param>
    private void UpdateRotation(Vector2 direction)
    {
        Transform animatorTransform = animator.transform;
        Vector3 rot = animatorTransform.localEulerAngles;
        float yRotation = Mathf.Atan2(direction.y, -direction.x) * Mathf.Rad2Deg;
        animatorTransform.localEulerAngles = new Vector3(rot.x, yRotation, rot.z);
    }
}