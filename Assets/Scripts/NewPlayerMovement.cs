using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerMovement : MonoBehaviour
{
    Playercontrols controls;
    private Rigidbody rb;
    public float speed = 0.7f;
    public float jumpForce = 10000000000f;
    public Vector2 inputVector;
    Animator animator;
    private bool Moving;

    private void Awake()
    {
        Playercontrols inputAction = new Playercontrols();
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        controls = new Playercontrols();
        controls.Gameplay.Move.performed += Movement_performed;

        controls.Gameplay.Move.canceled += ctx => { inputVector = Vector2.zero; Moving = false; };
        controls.Gameplay.Move.performed += ctx => Moving = true;
    }


    //Vector2 inputVector;
    private void Movement_performed(InputAction.CallbackContext obj)
    {
        Debug.Log(obj);
        inputVector = obj.ReadValue<Vector2>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        rb.velocity = (new Vector3(inputVector.x, 0f, 0f) * speed);

        if (Moving == true)
        {
            animator.SetBool("isMoving", true);
        }
        else if (Moving == false)
        {
            animator.SetBool("isMoving", false);
        }

    }


    void OnJump()
    {
        rb.AddForce(new Vector2(0, 1) * jumpForce, ForceMode.Impulse);
        Debug.Log("Salto");
    }

    void OnEnable()
    {
        controls.Gameplay.Move.Enable();
        controls.Gameplay.Jump.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Move.Disable();
        controls.Gameplay.Jump.Disable();
    }
}
