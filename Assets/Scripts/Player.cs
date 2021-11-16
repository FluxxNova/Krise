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
<<<<<<< Shadertest
    protected float axisX;
=======
    public float speedY;
    public float axisX;
    protected float axisY; 
>>>>>>> Prototipo+Shader
    public float gravityMultiplier = 2f;

    public float jumpForce = 500f;
    public float timeToDash = 1f;
    public float dashTime = 0.2f;
    public float currentTime;

    private Vector3 movePos;
    public Rigidbody rb;
    public float dashForce = 10f;
<<<<<<< Shadertest
    public float walljumpForceV = 300f;
    public float walljumpForceH = 500f;
    public float walljumpTime = 0.2f;
    public float walljumpTimer;
=======
>>>>>>> Prototipo+Shader
    public bool checkpoint1 = false;
    public bool god = true;

    public bool godmode = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lifes = maxlifes;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

    float oldTime;
    float oldTimeWallJump;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        


           

        if (currentTime > dashTime)
        {
            movePos.x = axisX * speed * Time.deltaTime;
            rb.MovePosition(transform.position + movePos);
<<<<<<< Shadertest

            //float clampedAxisX = Mathf.Clamp(axisX, -1f, 1f);
            //Vector3 newVelocity = Vector3.right * clampedAxisX * speed;
            //rb.AddForce(newVelocity, ForceMode.VelocityChange);
        }
        rb.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);
=======
>>>>>>> Prototipo+Shader

        }
        rb.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);

        currentTime += Time.deltaTime;
        walljumpTimer += Time.deltaTime;


        if (
            (oldTime < dashTime) &&     // Si en el fotograma anterior estaba dasheando...
<<<<<<< Shadertest
            (currentTime >= dashTime) ||
            (oldTimeWallJump < walljumpTime) &&
            (walljumpTimer >= walljumpTime)   // ... pero en este no estoy dasheando
=======
            (currentTime >= dashTime)
>>>>>>> Prototipo+Shader
            )
        {
            // Acabo de terminar de dashear
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }

        oldTime = currentTime;
        oldTimeWallJump = walljumpTimer;
        
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
        if (currentTime >= timeToDash)
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
<<<<<<< Shadertest

        if (wallTouched && walljumpTimer >= walljumpTime)
        {
            rb.AddForce(Vector3.up * walljumpForceV, ForceMode.VelocityChange);

            if (isFacingRight == false)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.right * walljumpForceH, ForceMode.VelocityChange);
            }
            if (isFacingRight == true)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.left * walljumpForceH, ForceMode.VelocityChange);
            }
            walljumpTimer = 0;
            
        }
=======

>>>>>>> Prototipo+Shader
        
    }
    public void GODMODE(){
        god = !god;
    }

    public override void Flip()
    {
        base.Flip();
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void ActiveGodMode()
    {
        godmode = !godmode;
    }
    

    private void OnTriggerEnter(Collider other)
    {
<<<<<<< Shadertest
        if (other.tag == "Damage" && god == false)
=======
        if (other.tag == "Damage" && godmode == false)
>>>>>>> Prototipo+Shader
        {
            Debug.Log("-1 vida");
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

        if (other.tag == "Map limit")
        {
            lifes = 0;
        }

<<<<<<< Shadertest
        else if (other.tag == "Checkpoint")
=======
        if (other.tag == "Checkpoint" && checkpoint1 == false)
>>>>>>> Prototipo+Shader
        {
            checkpoint1 = true;
            Debug.Log("Checkpoint");
        }

    }
}
