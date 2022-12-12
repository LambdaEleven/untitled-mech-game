using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechDash : MonoBehaviour
{
    private Rigidbody rigidBody;
    private MechMovementBase mainScript;
    private StateMachine sm;
    
    [Header("Dash")]
    public float dashForce;
    public float dashDuration;
    
    [Header("Cooldown")]
    public float dashCd;
    public float dashCdTimer;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        mainScript = GetComponent<MechMovementBase>();
        sm = GetComponent<StateMachine>();
    }

    private void Update()
    {
        if (sm.state == StateMachine.MovementState.dashing)
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
        mainScript.speedLimit = mainScript.dashSpeed;
        sm.dashing = true;
        Vector3 forceToApply = transform.forward * dashForce;
        delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);
        Invoke(nameof(ResetDash), dashDuration);
    }

    private Vector3 delayedForceToApply;
    
    private void DelayedDashForce()
    {
        rigidBody.AddForce(delayedForceToApply, ForceMode.Impulse);
    }

    private void ResetDash()
    {
        sm.dashing = false;
    }
}
