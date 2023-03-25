using System;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private InputProvider _inputProvider;

    /// moves an object in direction, multiplied
    /// by the movement speed of the object 
    public void MoveObject(Vector2 direction)
    {
        Vector3 offset = direction * moveSpeed;

        transform.position += offset;
    }

    /// moves object each physics step based on input provided by the input provider
    private void FixedUpdate()
    {
        MoveObject(_inputProvider.GetDirection() * Time.deltaTime);   
    }

    private void Awake()
    {
        _inputProvider = GetComponent<InputProvider>();
    }
}