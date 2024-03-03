using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 centerOffset;
    [SerializeField] float yPosition = 14f;
    [SerializeField] float rotationSpeed = 700f;
    [SerializeField] float scrollSpeed = 500f;
    [SerializeField] float radius = 5f;
    [SerializeField] float maxRadius = 10f;
    [SerializeField] float minRadius = 1.5f;

    [SerializeField] float angle;
    [SerializeField] float yAngle;
    [SerializeField] float yMaxLimit;
    [SerializeField] float yMinLimit;

    void Start()
    {
        centerOffset = player.GetComponentInChildren<CustomColliderBox>().CenterOffset;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            angle += -Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            yAngle += Input.GetAxis("Mouse Y") * rotationSpeed * 0.5f * Time.deltaTime;
            radius -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;
            //鏡頭距離上下限
            if (radius > maxRadius) radius = maxRadius;
            if (radius < minRadius) radius = minRadius;
            //Y軸上下限
            if (yAngle > yMaxLimit) yAngle = yMaxLimit;
            if (yAngle < yMinLimit) yAngle = yMinLimit;
        }
    }

    void LateUpdate()
    {
        transform.position = player.position + centerOffset +
        new Vector3(radius * Mathf.Cos(angle * Mathf.Deg2Rad) * Mathf.Cos(yAngle * Mathf.Deg2Rad),
                    radius * Mathf.Sin(yAngle * Mathf.Deg2Rad),
                    radius * Mathf.Sin(angle * Mathf.Deg2Rad) * Mathf.Cos(yAngle * Mathf.Deg2Rad));
        // transform.LookAt(player.transform.position + centerOffset);
        transform.LookAt(new Vector3(
            player.transform.position.x + centerOffset.x,
            yPosition,
            player.transform.position.z + centerOffset.z));
    }
}
