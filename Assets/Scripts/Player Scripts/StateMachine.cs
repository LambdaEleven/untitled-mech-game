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
    private Rigidbody rb;
    public bool busyInAction;
    public MovementState state;

    public enum MovementState
    {
        walking,
        flying,
        dashing
    }
    void Start()
    {
        moveScript = GetComponent<MechMove>();
        dashScript = GetComponent<MechDash>();
    }
    void Update()
    {
        StateHandler();
    }

    private void StateHandler()
    {
        if (Input.GetKey(sprintKey))
        {
            state = MovementState.flying; 
        }
        else
        {
            state = MovementState.walking;
        }

        if (dashScript.dashing)
        {
            state = MovementState.dashing;
        }
    }
}
