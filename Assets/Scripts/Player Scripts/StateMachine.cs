using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode dashKey = KeyCode.Space;

    private MechMove moveScript;
    private MechDash dashScript;
    private MechStats mStats;
    public bool boostExhausted;
    public MovementState state;

    public enum MovementState
    {
        Walking,
        Flying,
        Dashing
    }
    private void Start()
    {
        moveScript = GetComponent<MechMove>();
        dashScript = GetComponent<MechDash>();
        mStats = GetComponent<MechStats>();
    }
    private void Update()
    {
        StateHandler();
    }

    private void StateHandler()
    {
        if (((boostExhausted == false) & Input.GetKey(sprintKey)) & mStats.boost > 0)
        {
            state = MovementState.Flying; 
        }
        else
        {
            state = MovementState.Walking;
        }

        if (dashScript.dashing)
        {
            state = MovementState.Dashing;
        }
    }
}
