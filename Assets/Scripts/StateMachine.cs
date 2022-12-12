using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode dashKey = KeyCode.Space;
    
    public bool dashing;
    
    private MechMovementBase moveScript;
    private MechDash dashScript;
    public MovementState state;
    
    public enum MovementState
    {
        walking,
        flying,
        dashing
    }
    
    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<MechMovementBase>();
        dashScript = GetComponent<MechDash>();
    }

    // Update is called once per frame
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

        if ((Input.GetKeyDown(dashKey)) & state == MovementState.flying)
        {
            state = MovementState.dashing;
        }
    }
}
