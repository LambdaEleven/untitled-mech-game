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
        mainScript.speedLimit = mainScript.dashSpeed;
        mainScript.force = 0;
        Vector3 forceToApply = transform.forward * dashForce;
        delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.1f);
        Invoke(nameof(ResetDash), dashDuration);

        animator.SetBool("dashing",  true);
    }

    private Vector3 delayedForceToApply;
    
    private void DelayedDashForce()
    {
        rigidBody.AddForce(delayedForceToApply, ForceMode.Impulse);
    }

    private void ResetDash()
    {
        dashing = false;
        animator.SetBool("dashing", false);
        mainScript.force = 4;
    }
}
