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
    protected float axisX;

    public float jumpForce = 500f;
    public float timeToDash = 1f;
    public float dashTime = 0.2f;
    protected float currentTime;

    private Vector3 movePos;
    public Rigidbody rb;
    public float dashForce = 10f;
    public float walljumpForceV = 300f;
    public float walljumpForceH = 500f;
    public bool checkpoint1 = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lifes = maxlifes;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

    float oldTime;

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (currentTime > dashTime)
        {
            movePos.x = axisX * speed * Time.deltaTime;
            rb.MovePosition(transform.position + movePos);
        }
        //float clampedAxisX = Mathf.Clamp(axisX, -1f, 1f);
        //Vector3 newVelocity = Vector3.right * clampedAxisX * speed;
        //rb.AddForce(newVelocity, ForceMode.VelocityChange);


        currentTime += Time.deltaTime;


        if (
            (oldTime < dashTime) &&     // Si en el fotograma anterior estaba dasheando...
            (currentTime >= dashTime)   // ... pero en este no estoy dasheando
            )
        {
            // Acabo de terminar de dashear
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }

        oldTime = currentTime;

    }

    public void MovePlayer(float x)
    {
        axisX = x;
        if (axisX < 0 && isFacingRight || axisX > 0 && !isFacingRight)
            Flip();

        if (wallTouched)
        {
            if (isFacingRight && axisX > 0 || !isFacingRight && axisX < 0)
            {
                //axisX = 0;
            }

        }
    }
    public void Dash()
    {
        if (currentTime >= timeToDash)
        {
            
            if (isFacingRight == true)
            {
                rb.AddForce(Vector3.right * dashForce, ForceMode.VelocityChange);
            }
            if (isFacingRight == false)
            {
                rb.AddForce(Vector3.right * -dashForce, ForceMode.VelocityChange);
            }
            
            currentTime = 0;
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
    }

    public void Jump()
    {
        
        if (isGrounded)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        if (wallTouched)
        {
            rb.AddForce(Vector3.up * walljumpForceV, ForceMode.VelocityChange);

            if (isFacingRight == false)
                rb.AddForce(Vector3.right * walljumpForceH, ForceMode.VelocityChange);
            if (isFacingRight == true)
                rb.AddForce(Vector3.left * walljumpForceH, ForceMode.VelocityChange);
        }
        
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
        if (other.tag == "Damage")
        {
            Debug.Log("Estas muerto wey");
            lifes--;

            if (lifes <= 0)
            {
                if (checkpoint1 == true)
                {
                    Debug.Log("Vuelve a intentarlo");
                }
                else
                    Debug.Log("se acabo");
            }
        }
        else if (other.tag == "Checkpoint")
        {
            checkpoint1 = true;
            Debug.Log("Checkpoint");
        }

    }
}
