using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechDash : MonoBehaviour
{
    private Rigidbody rigidBody;
    private MechMovement1 mainScript;
    [Header("Dash")]
    public float dashForce;
    public float dashDuration;
    public float dashCd;
    public float dashCdTimer;
    public KeyCode dashKey = KeyCode.Space;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        mainScript = GetComponent<MechMovement1>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey))
        {
            Dash();
        }
    }

    private void Dash()
    {
        Vector3 forceToApply = transform.forward * dashForce;
        rigidBody.AddForce(forceToApply, ForceMode.Impulse);
        Invoke(nameof(ResetDash), dashDuration);
    }

    private void ResetDash()
    {
        
    }
}
