using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PhysicsCollision
{
    [Header("Player Lifes")]
    public int maxlifes = 3;
    public int lifes;

    [Header("Move Parameters")]
    public float speed;
    public float speedY;
    public float axisX;
    protected float axisY; 
    public float gravityMultiplier = 4f;

    public float jumpForce = 15f;
    public float timeToDash = 1f;
    public float dashTime = 0.2f;
    private float currentTime;

    private Vector3 movePos;
    public Rigidbody rb;
    public GodMode godMode;
    public float dashForce = 10f;
    public bool checkpoint1 = false;

    private Vector3 velocidad;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lifes = maxlifes;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

    float oldTime;

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
            // Acabo de terminar de dashear
            /*for(dashSlow = 0; dashSlow <= timeToDash; dashSlow = currentTime)
            {
                rb.velocity = rb.velocity / 2;
            }*/
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
        

        /*if (wallTouched)
        {
            if (isFacingRight && axisX > 0 || !isFacingRight && axisX < 0)
            {
                //axisX = 0; //Dejo de caminar hacia la pared
            }

        }*/
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
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

        
    }

    public override void Flip()
    {
        base.Flip();
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Damage" && godMode.isInmortal == false)
        {
            Debug.Log("-1 vida");
            lifes--;
        }

        if (other.tag == "Map limit" && godMode.isInmortal == false)
        {
            lifes = 0;
        }

        if (other.tag == "Checkpoint" && checkpoint1 == false)
        {
            checkpoint1 = true;
        }

    }
}
