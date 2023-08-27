using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private int speed = 300;
    private bool _isMoving = false;

    void Update()
    {
        if (_isMoving)
            return;

        var direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            direction = Vector3.forward;
        else if (Input.GetKey(KeyCode.A))
            direction = Vector3.left;
        else if (Input.GetKey(KeyCode.S))
            direction = Vector3.back;
        else if (Input.GetKey(KeyCode.D))
            direction = Vector3.right;

        if (direction == Vector3.zero)
            return;

        StartCoroutine(Roll(direction));
    }

    IEnumerator Roll(Vector3 direction)
    {
        _isMoving = true;
        float remainingAngle = 90;
        var rotationCenter = transform.position + direction / 2 + Vector3.down / 2;
        var rotationAxis = Vector3.Cross(Vector3.up, direction);

        while (remainingAngle > 0)
        {
            float rotationAngle = Math.Min(Time.deltaTime * this.speed, remainingAngle);
            transform.RotateAround(rotationCenter, rotationAxis, rotationAngle);
            remainingAngle -= rotationAngle;
            yield return null;
        }

        _isMoving = false;
    }
}