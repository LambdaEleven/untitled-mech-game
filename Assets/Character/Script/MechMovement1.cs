using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechMovement1 : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float dashSpeed;
    public Transform cursorTarget;
    public float force;
    public float groundDrag;
    public float airDrag;
    
    private Rigidbody rigidBody;
    private Animator animator;
    private float speedLimit;
    float horizontalInput;
    float verticalInput;
    Vector3 movement;
    public bool dashing;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rigidBody.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();
        AnimatePlayer();
        SpeedControl();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedLimit = runSpeed;
            rigidBody.drag = airDrag;
            animator.SetBool("moveSpeed", true);
        }
        else
        {
            speedLimit = walkSpeed;
            rigidBody.drag = groundDrag;
            animator.SetBool("moveSpeed", false);
        }
    }

    private void FixedUpdate()
    {
        MovePLayer();
        
        if (movement.magnitude > 0)
        rigidBody.AddForce(movement.normalized * (speedLimit * force), ForceMode.Force);
    }

    private void MyInput()
    {
        //Look at Cursor Target
        transform.LookAt(new Vector3(cursorTarget.position.x, transform.position.y, cursorTarget.position.z));
        
        //Obtaining Movement Input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePLayer()
    {
        movement = new Vector3(horizontalInput, 0f, verticalInput);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

        if (flatVel.magnitude > speedLimit)
        {
            Vector3 limitedVel = flatVel.normalized * speedLimit;
            rigidBody.velocity = new Vector3(limitedVel.x, rigidBody.velocity.y, limitedVel.z);
        }
    }

    private void AnimatePlayer()
    { 
        //Movement Animations
        float velocityX = Vector3.Dot(movement.normalized, transform.right);
        float velocityZ = Vector3.Dot(movement.normalized, transform.forward);
        
        animator.SetFloat("velocityX", velocityX);
        animator.SetFloat("velocityZ", velocityZ);
    }
}
