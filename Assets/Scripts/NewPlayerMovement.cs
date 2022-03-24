using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerMovement : MonoBehaviour
{
    Playercontrols controls;
    private Rigidbody rb;
    public float speed = 1f;
    public Vector2 inputVector;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        controls = new Playercontrols();
        controls.Gameplay.Move.performed += Movement_performed;
    }


    private void Movement_performed(InputAction.CallbackContext obj)
    {
        Debug.Log(obj);
        Vector2 inputVector2 = obj.ReadValue<Vector2>();
        float speed2 = 0.5f;
        rb.AddForce(new Vector3(inputVector2.x, 0f, 0f) * speed, ForceMode.Force);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputVector = controls.Gameplay.Move.ReadValue<Vector2>();
        rb.AddForce(new Vector3(inputVector.x, 0f, 0f) * speed, ForceMode.VelocityChange);
        controls.Gameplay.Move.canceled += ctx => inputVector = Vector2.zero;
    }


    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
