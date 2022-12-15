using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechMove : MonoBehaviour
{
    [Header("Movement")] public float walkSpeed;
    public float runSpeed;
    public float speedLimit;
    private float boostDrain = 8f;
    [HideInInspector] public float boostRegen = 30f;

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
    private MechDash dashScript;
    private MechStats mStats;

    void Start()
    {
        sm = GetComponent<StateMachine>();
        dashScript = GetComponent<MechDash>();
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        mStats = GetComponent<MechStats>();
        rigidBody.freezeRotation = true;
    }

    private void Update()
    {
        PlayerInput();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        if (sm.state == StateMachine.MovementState.Walking)
        {
            PlayerWalk();
            if (mStats.boost < mStats.maxBoost)
            {
                mStats.boost += boostRegen * Time.deltaTime;
            }
        }

        if (sm.state == StateMachine.MovementState.Flying)
        {
            PlayerFly();
            animator.SetBool("moveSpeed", true);
            mStats.boost -= boostDrain * Time.deltaTime;
        }
        else
        {
            animator.SetBool("moveSpeed", false);
        }
    }

    private void PlayerInput()
    {
        //Look at Cursor Target, Disabled while in Dashing state
        if (sm.state != StateMachine.MovementState.Dashing)
        {
            transform.LookAt(new Vector3(cursorTarget.position.x, transform.position.y, cursorTarget.position.z));
        }

        //Obtaining Movement Input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    //SpeedControl prevents player from moving faster than speedLimit, Useful when force is in use
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

        if (flatVel.magnitude > speedLimit)
        {
            Vector3 limitedVel = flatVel.normalized * speedLimit;
            rigidBody.velocity = new Vector3(limitedVel.x, rigidBody.velocity.y, limitedVel.z);
        }
    }
    //Is enabled when player is in "walking" state
    private void PlayerWalk()
    {
        speedLimit = walkSpeed;
        rigidBody.drag = groundDrag;
        movement = new Vector3(horizontalInput, 0f, verticalInput);
        //Force only added when Player is moving
        if (movement.magnitude > 0)
            rigidBody.AddForce(movement.normalized * (speedLimit * force), ForceMode.Force);
        //Movement Animations
        float velocityX = Vector3.Dot(movement.normalized, transform.right);
        float velocityZ = Vector3.Dot(movement.normalized, transform.forward);

        animator.SetFloat("velocityX", velocityX);
        animator.SetFloat("velocityZ", velocityZ);
    }
    //Is enabled when player is in "flying" state
    private void PlayerFly()
    {
        speedLimit = runSpeed;
        rigidBody.drag = airDrag;
        movement = new Vector3(horizontalInput, 0f, verticalInput);
        //Force only added when Player is moving
        if (movement.magnitude > 0)
            rigidBody.AddForce(movement.normalized * (speedLimit * force), ForceMode.Force);
        //Movement Animations
        float velocityX = Vector3.Dot(movement.normalized, transform.right);
        float velocityZ = Vector3.Dot(movement.normalized, transform.forward);

        animator.SetFloat("velocityX", velocityX);
        animator.SetFloat("velocityZ", velocityZ);
    }
}