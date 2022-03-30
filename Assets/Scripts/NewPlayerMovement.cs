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
    public float jumpForce;
    public float dashForce;
    public Vector2 inputVector;
    Animator animator;
    private bool Moving;

    Vector2 dashMovement;


    [SerializeField] CharacterController controller;
    Vector2 horizontalInput;
    public bool jump;
    public bool dash;
    float dashTime = 0.2f;
    float timeToDash = 1f;
    public float currentTime;
    [SerializeField] float gravity = -30f; // -9.81
    Vector3 verticalVelocity = Vector3.zero;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask wallMask;
    public bool isGrounded;
    public bool isFacingLeft;


    private void Awake()
    {
        Playercontrols inputAction = new Playercontrols();
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        controller = GetComponent<CharacterController>();

        controls = new Playercontrols();
        controls.Gameplay.Move.performed += Movement_performed;
        controls.Gameplay.Jump.performed += OnJump;
        controls.Gameplay.Dash.performed += OnDash;

        controls.Gameplay.Move.canceled += ctx => { inputVector = Vector2.zero; Moving = false; };
        controls.Gameplay.Move.performed += ctx => Moving = true;
    }


    //Vector2 inputVector;
    private void Movement_performed(InputAction.CallbackContext obj)
    {
        //Debug.Log(obj);
        inputVector = obj.ReadValue<Vector2>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        isGrounded = Physics.CheckSphere(transform.position, 1f, groundMask);
        if (isGrounded)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }

        //rb.velocity = (new Vector3(inputVector.x, 0f, 0f) * speed);

        Vector2 horizontalVelocity = (transform.right * inputVector.x + transform.forward * inputVector.y) * speed;
        controller.Move(horizontalVelocity * Time.deltaTime);

        if (Moving == true)
        {
            animator.SetBool("isMoving", true);
        }
        else if (Moving == false)
        {
            animator.SetBool("isMoving", false);
        }

        

        if (inputVector.x > 0f && isFacingLeft)
        {
            Flip();
        }
        if (inputVector.x < 0f && !isFacingLeft)
        {
            Flip();
        }

        
        if (currentTime >= dashTime)
        {
            dashMovement.x = 0f;
            speed = 13;
            dash = false;
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
        controller.Move(dashMovement * Time.deltaTime);
    }


    void OnJump(InputAction.CallbackContext isJumping)
    {
        jump = true;
        if (jump)
        {
            if (isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpForce * gravity);
                Debug.Log("Salto");
            }
            jump = false;
        }
    }
    void OnDash(InputAction.CallbackContext isDashing)
    {
        dash = true;
        if (dash == true && currentTime > timeToDash)
        {
            if (isFacingLeft)
                dashMovement.x = -1 * dashForce;
            if (!isFacingLeft)
                dashMovement.x = 1 * dashForce;
            currentTime = 0f;
            Debug.Log("Dasheo");
        }
    }

    void OnEnable()
    {
        controls.Gameplay.Move.Enable();
        controls.Gameplay.Jump.Enable();
        controls.Gameplay.Dash.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Move.Disable();
        controls.Gameplay.Jump.Disable();
        controls.Gameplay.Dash.Disable();
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        isFacingLeft = !isFacingLeft;
    }
}
