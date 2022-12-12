using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MechMovement : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public Transform cursorTarget;
    Rigidbody rigidBody;
    Animator animator;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        //Look at Cursor Target
        transform.LookAt(new Vector3(cursorTarget.position.x, transform.position.y, cursorTarget.position.z));
        
        //Obtaining Movement Input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        
        //Walking or Sprinting
        if ((movement.magnitude > 0) & (Input.GetKey(KeyCode.LeftShift)))
        {
            movement.Normalize();
            movement *= runSpeed * Time.deltaTime;
            transform.Translate(movement, Space.World);
            animator.SetBool("moveSpeed", true);
            
        }
        else if (movement.magnitude > 0)
        {
            movement.Normalize();
            movement *= walkSpeed * Time.deltaTime;
            transform.Translate(movement, Space.World);
            animator.SetBool("moveSpeed", false);
        }
        
        //Movement Animations
        float velocityX = Vector3.Dot(movement.normalized, transform.right);
        float velocityZ = Vector3.Dot(movement.normalized, transform.forward);
        
        animator.SetFloat("velocityX", velocityX);
        animator.SetFloat("velocityZ", velocityZ);
    }
}
