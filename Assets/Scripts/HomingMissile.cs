using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D rigidBody;
    public float angleChangingSpeed;
    public float movementSpeed;
    public NewPlayerMovement player;
    private void Awake()
    {
        player = FindObjectOfType<NewPlayerMovement>();
        target = player.transform;
    }
    void start()
    {
    }
    void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - rigidBody.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rigidBody.angularVelocity = -angleChangingSpeed * rotateAmount;
        rigidBody.velocity = transform.up * movementSpeed;
    }
}