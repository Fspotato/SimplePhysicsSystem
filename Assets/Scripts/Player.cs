using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    Animator animator;
    CustomRigidBody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponentInChildren<CustomRigidBody>();
    }

    void Update()
    {
        MoveControll();
    }


    void MoveControll()
    {
        animator.SetBool("Run", true);

        rb.velocity.x = Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad) * moveSpeed;
        rb.velocity.z = Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad) * moveSpeed;

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y - 45, 0);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y + 45, 0);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y - 135, 0);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y + 135, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y + 90, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y + 180, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y - 90, 0);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
        }
        else
        {
            animator.SetBool("Run", false);
            rb.velocity.x = 0;
            rb.velocity.z = 0;
        }
    }
}
