using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerMovement : MonoBehaviour
{
    Playercontrols controls;
    private Rigidbody rb;
    public float speed = 0.7f;
    public Vector2 inputVector;
    Animator animator;
    private bool Moving;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        controls = new Playercontrols();
        controls.Gameplay.Move.performed += Movement_performed;
    }


    private void Movement_performed(InputAction.CallbackContext obj)
    {
        Debug.Log(obj);
        Vector2 inputVector2 = obj.ReadValue<Vector2>();
        float speed2 = 0.5f;
        rb.AddForce(new Vector3(inputVector2.x, 0f, 0f) * speed2, ForceMode.Force);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        inputVector = controls.Gameplay.Move.ReadValue<Vector2>();
        rb.velocity = (new Vector3(inputVector.x, 0f, 0f) * speed);
        controls.Gameplay.Move.canceled += ctx => inputVector = Vector2.zero;
        controls.Gameplay.Move.canceled += ctx => Moving = false;
        controls.Gameplay.Move.performed += ctx => Moving = true;


        if (Moving == true)
        {
            animator.SetBool("isMoving", true);
        }
        else if (Moving == false)
        {
            animator.SetBool("isMoving", false);
        }
    }
    // Update is called once per frame

    void OnEnable()
    {
        controls.Gameplay.Move.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Move.Disable();
    }
}
