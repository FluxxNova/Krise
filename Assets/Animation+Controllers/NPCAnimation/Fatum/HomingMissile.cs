using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public Transform target;
    public Rigidbody rigidBody;
    public float angleChangingSpeed;
    public float movementSpeed;
    public NewPlayerMovement player;
    public Vector3 TurnMultiplier;
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
        Vector3 direction = (Vector3)target.position - rigidBody.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        TurnMultiplier.z = -angleChangingSpeed * rotateAmount;

        rigidBody.angularVelocity = TurnMultiplier;
        rigidBody.velocity = transform.up * movementSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ground")
        {
            Destroy(gameObject);
        }
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}