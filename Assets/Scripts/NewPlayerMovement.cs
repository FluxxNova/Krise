using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class NewPlayerMovement : MonoBehaviour
{
    [Header("Definitions")]
    [SerializeField] CharacterController controller;
    public Playercontrols controls;
    Animator animator;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask wallMask;
    [SerializeField] LayerMask enemyLayer;

    [Header("HP and WINLOSE")]
    public int maxlifes = 3;
    public int lifes = 3;
    public Image lifebar;
    public bool isDead;
    public bool checkpoint1 = false;

    [Header("Move Parameters")]
    public Vector2 inputVector;
    public float speed = 0.7f;
    public float jumpForce;
    public float dashForce;
    Vector2 dashMovement;
    Vector2 horizontalInput;
    Vector2 verticalVelocity = Vector2.zero;
    [SerializeField] float gravity = -20f; // -9.81
    protected float rayDistance = 1f;
    protected float rayOffset = 0.5f;
    private Vector3 lookDirection = Vector3.right;
    [SerializeField] float terminalVerticalVelocity = -5f;

    [Header("Booleans")]
    public bool jumping;
    public bool wallTouched;
    public bool isFacingLeft;
    public bool jump;
    public bool dash;
    private bool Moving;
    public bool fly;

    [Header("Time Parameters")]
    private float currentTime;
    float dashTime = 0.2f;
    float timeToDash = 1f;
    private float damageCD = 2f;
    public float damageTime;

    [Header("Attack Parameters")]
    public GameObject attack;
    public float attackRange;

    [Header("Controller declaration")]
    public GodMode godMode;
    public GameManager gameManager;
    public AudioManager audioManager;
    public Titania titania;
    public Fatum fatum;
    public Enemy golem;
    void Start()
    {
        fatum = FindObjectOfType<Fatum>();
        golem = FindObjectOfType<Enemy>();
        lifes = maxlifes;
        gameManager = FindObjectOfType<GameManager>();
        audioManager = GetComponentInChildren<AudioManager>();
        //Cursor.visible = false;
    }
    float originalZ;

    private void Awake()
    {
        Playercontrols inputAction = new Playercontrols();
        animator = GetComponentInChildren<Animator>();
        originalZ = transform.position.z;
        controller = GetComponent<CharacterController>();

        controls = new Playercontrols();
        controls.Gameplay.Move.performed += Movement_performed;
        controls.Gameplay.Jump.performed += OnJump;
        controls.Gameplay.Dash.performed += OnDash;
        controls.Gameplay.Pause.performed += OnPause;
        controls.Gameplay.Attack.performed += OnAttack;

        controls.Gameplay.Move.canceled += ctx => { inputVector = Vector2.zero; Moving = false; };
        controls.Gameplay.Move.performed += ctx => Moving = true;
    }


    private void Movement_performed(InputAction.CallbackContext obj)
    {
        inputVector = obj.ReadValue<Vector2>();
    }
    // Start is called before the first frame update


    private void FixedUpdate()
    {
        WallChecker();
        currentTime += Time.deltaTime;
        damageTime += Time.deltaTime;
        //isGrounded = Physics.CheckSphere(transform.position, 0.2f, groundMask);
        controller.transform.position = new Vector3(transform.position.x, transform.position.y, -6.5f);

        Vector2 horizontalVelocity = (transform.right * inputVector.x + transform.forward * inputVector.y) * speed;

        Vector2 compositeMovement = Vector2.zero;



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

        animator.SetBool("isJumping", !controller.isGrounded);

        if (currentTime >= dashTime)
        {
            dashMovement.x = 0f;
            speed = 13;
            dash = false;
        }

        if (wallTouched)
        {
            verticalVelocity = (transform.up * inputVector.y + transform.forward * inputVector.y) * speed;
        }
        else
        {
            verticalVelocity.y += gravity * Time.deltaTime;
            if (verticalVelocity.y < terminalVerticalVelocity)
            {
                verticalVelocity.y = terminalVerticalVelocity;
            }
        }


        if (fly)
        {
            wallTouched = true;
            verticalVelocity = (transform.up * inputVector.y + transform.forward * inputVector.y) * speed;
        }
        else
        {
            gravity = -30;
        }

        compositeMovement += verticalVelocity * Time.deltaTime;
        compositeMovement += dashMovement * Time.deltaTime;
        compositeMovement += horizontalVelocity * Time.deltaTime;

        controller.Move(compositeMovement);

        Vector3 newPosition = transform.position;
        newPosition.z = originalZ;
        transform.position = newPosition;
    }
    void OnAttack(InputAction.CallbackContext isAttacking)
    {

        Collider[] hitEnemies = Physics.OverlapSphere(attack.transform.position, attackRange, enemyLayer);
        audioManager.PlayClip(3);
        animator.SetTrigger("Attack");
        foreach (Collider enemy in hitEnemies)
        {
            enemy.transform.SendMessage("GetDamage");
            audioManager.PlayClip(5);
        }
    }


    void OnJump(InputAction.CallbackContext isJumping)
    {
        jump = true;
        if (jump && controller.isGrounded)
        {
            audioManager.PlayClip(2);
            //verticalVelocity.y = Mathf.Sqrt(-2f * jumpForce * gravity);
            verticalVelocity.y = jumpForce;
            jump = false;
        }
    }
    public void OnPause(InputAction.CallbackContext obj)
    {
        gameManager.CloseSettings();
        gameManager.Pause();
        audioManager.PlayClip(6);
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
        }
    }

    void OnEnable()
    {
        controls.Gameplay.Move.Enable();
        controls.Gameplay.Jump.Enable();
        controls.Gameplay.Dash.Enable();
        controls.Gameplay.Pause.Enable();
        controls.Gameplay.Attack.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Move.Disable();
        controls.Gameplay.Jump.Disable();
        controls.Gameplay.Pause.Disable();
        controls.Gameplay.Dash.Disable();
        controls.Gameplay.Attack.Disable();
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        lookDirection *= -1;
        isFacingLeft = !isFacingLeft;
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Damage" && godMode.isInvulnerable == false)
        {
            if (damageTime > damageCD)
            {
                audioManager.PlayClip(1);
                Debug.Log("-1 vida");
                lifes--;
                lifebar.fillAmount -= 0.34f;
                damageTime = 0f;
            }

            if (lifes <= 0)
            {
                gameManager.Die();
            }

        }

        if (other.tag == "Win")
        {
            isDead = true;
            gameManager.Win();
            Cursor.visible = true;
        }

        if (other.tag == "Map limit" && godMode.isInvulnerable == false)
        {
            lifes = 0;
            gameManager.Die();
        }

        if (other.tag == "Checkpoint" && checkpoint1 == false)
        {
            audioManager.PlayClip(4);
            checkpoint1 = true;
        }

        if (other.tag == "TargetDetect")
        {
            titania.targetDetected = true;
        }

    }
    private void WallChecker()
    {
        wallTouched = false;

        Vector3 rayPos = Vector3.zero;
        int mult = 0;

        for (int i = 0; i < 3; i++)
        {
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(transform.position + rayPos, lookDirection, out hit, 1, wallMask))
            {
                wallTouched = true;
                break;
            }

            if (mult <= 0)
            {
                mult *= -1;
                mult++;
            }
            else mult *= -1;

            rayPos.y = mult * rayOffset;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "FatumDetect")
        {
            fatum.Fire();
        }
        if (other.tag == "GolemDetect")
        {
            golem.Attack();
        }
    }
}

