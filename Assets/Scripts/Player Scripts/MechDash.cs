using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechDash : MonoBehaviour
{
    private Rigidbody rigidBody;
    private MechMovementBase mainScript;
    private StateMachine sm;
    private Animator animator;
    
    [Header("Dash")]
    public float dashForce;
    public float dashDuration;
    public float dashSpeed;
    public bool dashing;
    
    
    [Header("Cooldown")]
    public float dashCd;
    public float dashCdTimer;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        mainScript = GetComponent<MechMovementBase>();
        sm = GetComponent<StateMachine>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Dash ability only works while flying
        if ((Input.GetKey(sm.dashKey)) & sm.state == StateMachine.MovementState.flying)
        {
            Dash();
        }

        if (dashCdTimer > 0)
        {
            dashCdTimer -= Time.deltaTime;
        }
    }

    private void Dash()
    {
        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCd;
        dashing = true;
        mainScript.force = 0;
        Vector3 forceToApply = transform.forward * dashForce;
        delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.1f);
        Invoke(nameof(ResetDash), dashDuration);

        animator.SetBool("dashing",  true);
    }

    private Vector3 delayedForceToApply;
    
    //DelayedDashForce handles dash startup, and prevents conflicts with walking/flying force
    private void DelayedDashForce()
    {
        mainScript.speedLimit = dashSpeed;
        rigidBody.AddForce(delayedForceToApply, ForceMode.Impulse);
    }

    //ResetDash handles dash cooldown, and returns player back to actionable state
    private void ResetDash()
    {
        dashing = false;
        animator.SetBool("dashing", false);
        mainScript.force = 4;
    }
}
