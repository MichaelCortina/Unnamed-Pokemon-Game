using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Component causing this object to inherit only the position changes of the follow target. Rotation and scale are
/// not inherited and this object will not be a child to the follow object.
/// </summary>
public class FollowObject : MonoBehaviour
{
    [SerializeField] private GameObject followTarget;
    private Vector3 _relativePosition;
    
    private void Start()
    {
        _relativePosition = transform.position - followTarget.transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = followTarget.transform.position + _relativePosition;
    }
}
