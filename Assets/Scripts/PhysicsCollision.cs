using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCollision : MonoBehaviour
{
    public LayerMask groundLayer;
    protected bool isGrounded = false;
    protected bool wasGrounded = false;
    protected bool justGrounded = false;

    public LayerMask wallLayer;
    protected bool wallTouched = false;
    protected bool wasTouchingWall = false;
    protected bool justTouchingWall = false;
    protected bool isFacingRight = true;
    private Vector3 lookDirection = Vector3.right;
    
    [Space]
    protected float rayDistance = 1f;
    protected float rayOffset = 0.5f;
    Ray ray;

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        GroundChecker();
        WallChecker();
    }

    private void GroundChecker()
    {
        //Vector3.down = new Vector3(0,-1, 0)
        //ray = new Ray(transform.position, Vector3.down);
        //bool rayHit = Physics.Raycast(ray, 1, groundLayer);
        wasGrounded = isGrounded;
        isGrounded = false;
        justGrounded = false;

        Vector3 rayPos = Vector3.zero;
        int mult = 0;

        for (int i = 0; i < 3; i++)
        {
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(transform.position + rayPos, Vector3.down, out hit, 1, groundLayer))
            {
                isGrounded = true;
                if (!wasGrounded)
                    justGrounded = true;
                break;
            }

            if (mult <= 0)
            {
                mult *= -1;
                mult++;
            }
            else mult *= -1;

            rayPos.x = mult * rayOffset;
        }
    }

    private void WallChecker()
    {
        //Vector3.down = new Vector3(0,-1, 0)
        //ray = new Ray(transform.position, Vector3.down);
        //bool rayHit = Physics.Raycast(ray, 1, groundLayer);
        wasTouchingWall = wallTouched;
        wallTouched = false;
        justTouchingWall = false;

        Vector3 rayPos = Vector3.zero;
        int mult = 0;

        for (int i = 0; i < 3; i++)
        {
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(transform.position + rayPos, lookDirection, out hit, 1, wallLayer))
            {
                wallTouched = true;
                if (!wasTouchingWall)
                    justTouchingWall = true;
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

    public virtual void Flip()
    {
        isFacingRight = !isFacingRight;
        lookDirection.x *= -1;
    }

    private void OnDrawGizmos()
    {
        DrawGroundChecker();
        DrawWallChecker();
    }

    private void DrawGroundChecker()
    {
        Gizmos.color = Color.cyan;
        int mult = 0;
        Vector3 pos = Vector3.zero;

        for (int i = 0; i < 3; i++)
        {
            Vector3 drawPos = transform.position + pos;
            Gizmos.DrawLine(drawPos, drawPos + Vector3.down);
            if (mult <= 0)
            {
                mult *= -1;
                mult++;
            }
            else mult *= -1;

            pos.x = mult * rayOffset;
        }
    }

    private void DrawWallChecker()
    {
        Gizmos.color = Color.cyan;
        int mult = 0;
        Vector3 pos = Vector3.zero;

        for (int i = 0; i < 3; i++)
        {
            Vector3 drawPos = transform.position + pos;
            Gizmos.DrawLine(drawPos, drawPos + lookDirection);
            if (mult <= 0)
            {
                mult *= -1;
                mult++;
            }
            else mult *= -1;

            pos.y = mult * rayOffset;
        }
    }
}
