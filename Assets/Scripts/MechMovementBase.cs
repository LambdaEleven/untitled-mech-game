using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechMovementBase : MonoBehaviour
{
    [Header("Movement")]
     public float walkSpeed;
    public float runSpeed;
    public float dashSpeed;
    [HideInInspector] public float speedLimit;

    [Header("Physics")]
    public float force;
    public float groundDrag;
    public float airDrag;
    
    [Header("Cursor")]
    public Transform cursorTarget;

    [Header("Keybinds")]
    public KeyCode sprintKey = KeyCode.LeftShift;
    
    private Rigidbody rigidBody;
    private Animator animator;
    float horizontalInput;
    float verticalInput;
    Vector3 movement;
    private StateMachine sm;

    void Start()
    {
        sm = GetComponent<StateMachine>();
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rigidBody.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();
        AnimatePlayer();
        SpeedControl();

        if (sm.state == StateMachine.MovementState.flying)
        {
            speedLimit = runSpeed;
            rigidBody.drag = airDrag;
            animator.SetBool("moveSpeed", true);
        }
        if (sm.state == StateMachine.MovementState.walking)
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
