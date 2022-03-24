using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : PhysicsCollision
{
    [Header("HP and WINLOSE")]
    public int maxlifes = 3;
    public int lifes = 3;
    public Image lifebar;
    public bool isDead;
    public bool checkpoint1 = false;

    [Header("Move Parameters")]
    public float speed;
    public float speedY;
    public float axisX;
    protected float axisY; 
    public float gravityMultiplier = 4f;
    public float jumpForce = 15f;
    private Vector3 movePos;

    [Header("Dash Parameters")]
    public float timeToDash = 1f;
    public float dashTime = 0.2f;
    public float dashForce = 10f;

    [Header("Time Parameters")]
    private float currentTime;
    float oldTime;

    [Header("attack Parameters")]
    public GameObject attack;
    public float attackRange;

    [Header("Controller declaration")]
    public Rigidbody rb;
    public GodMode godMode;
    private GameManager gameManager;
    public AudioManager audioManager;
    public LayerMask enemyLayer;

    [Header("Debug Parameters")]
    public Vector3 velocidad;
    

    void Start()
    {
        lifes = maxlifes;
        gameManager = FindObjectOfType<GameManager>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        audioManager = GetComponentInChildren<AudioManager>();
        Cursor.visible = false;
    }

    

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (currentTime > dashTime)
        {
            movePos.x = axisX * speed * Time.deltaTime;
            if(godMode.canFly)
                movePos.y = axisY * speed * Time.deltaTime;
            rb.MovePosition(transform.position + movePos);

        }
        rb.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);

        currentTime += Time.deltaTime;


        if (
            (oldTime < dashTime) &&     // Si en el fotograma anterior estaba dasheando...
            (currentTime >= dashTime)
            )
        {
           
            rb.velocity = Vector3.zero;
            if (isFacingRight == true)
                rb.AddForce(Vector3.right * dashForce/5, ForceMode.VelocityChange);
            if (isFacingRight == false)
                rb.AddForce(Vector3.right * -dashForce/5, ForceMode.VelocityChange);
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }

        oldTime = currentTime;
        velocidad = rb.velocity;

    }

    public void MovePlayerX(float x)
    {
        axisX = x;
        if (axisX < 0 && isFacingRight || axisX > 0 && !isFacingRight)
            Flip();
        

        if (wallTouched && godMode == false)
        {
            if (isFacingRight && axisX > 0 || !isFacingRight && axisX < 0)
            {
                axisX = 0; //Dejo de caminar hacia la pared
            }

        }
    }

    public void MovePlayerY(float y)
    {
        axisY = y;
        if (wallTouched) // Escalar mirando hacia la derecha 
        {
            rb.velocity = new Vector3(rb.velocity.x, axisY * speedY);
        }
        
    }
    public void Dash()
    {
        if (currentTime >= timeToDash && godMode.canFly == false)
        {
            audioManager.PlayClip(0);
            if (isFacingRight == true)
                rb.AddForce(Vector3.right * dashForce, ForceMode.VelocityChange);
            if (isFacingRight == false)
                rb.AddForce(Vector3.right * -dashForce, ForceMode.VelocityChange);
            
            currentTime = 0;
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
    }

    public void Jump()
    {

        if (isGrounded && !wallTouched)
        {
            audioManager.PlayClip(2);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }


    }

    public override void Flip()
    {
        base.Flip();
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Attack()
    {
       Collider[] hitEnemies = Physics.OverlapSphere(attack.transform.position, attackRange, enemyLayer);
        audioManager.PlayClip(3);
        foreach (Collider enemy in hitEnemies)
        {
            enemy.transform.SendMessage("GetDamage");
            Debug.Log("He Hiteao");
            audioManager.PlayClip(5);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attack.transform.position, attackRange);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Damage" && godMode.isInvulnerable == false)
        {
            audioManager.PlayClip(1);
            Debug.Log("-1 vida");
            lifes--;
            lifebar.fillAmount -= 0.34f;

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

    }
}
