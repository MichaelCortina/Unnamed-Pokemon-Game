using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBoundObject : MonoBehaviour
{
    [SerializeField] private float maxHeight;
    [SerializeField] private Grid grid;
    [SerializeField] private LayerMask groundLayers;

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        Vector3 origin = pos;
        origin.y = maxHeight;
        Physics.Raycast(origin, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundLayers);
        transform.position = new Vector3(pos.x, hit.point.y, pos.z);
    }
}
