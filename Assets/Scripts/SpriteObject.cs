using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteObject : MonoBehaviour
{
    [SerializeField] private bool orientToCamera = true;
    [SerializeField] private new Camera camera;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (orientToCamera) transform.rotation = camera.transform.rotation;
    }
}
