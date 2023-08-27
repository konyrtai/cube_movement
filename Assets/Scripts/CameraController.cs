using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private float rotationSpeed = 2;
    [SerializeField] private float distance = 5;

    [SerializeField] private bool invertX;
    [SerializeField] private bool invertY;

    [SerializeField] private float minVerticalAngle = -45;
    [SerializeField] private float maxVerticalAngle = 45;

    [SerializeField] private Vector2 framingOffset;

    private float _rotationX;
    private float _rotationY;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    { 
        _rotationX += Input.GetAxis("Mouse Y") * rotationSpeed * (invertX ? 1 : -1);
        _rotationY += Input.GetAxis("Mouse X") * rotationSpeed * (invertY ? -1 : 1);

        _rotationX = Mathf.Clamp(_rotationX, minVerticalAngle, maxVerticalAngle);
        
        var targetRotation = Quaternion.Euler(_rotationX, _rotationY, 0);
        var focusPosition = followTarget.position + new Vector3(framingOffset.x, framingOffset.y);
        transform.position = focusPosition  - targetRotation * new Vector3(0, 0, distance);
        transform.rotation = targetRotation;
    }
}