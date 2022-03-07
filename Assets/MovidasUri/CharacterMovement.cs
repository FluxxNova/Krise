using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController _controller;
    private float ActualTime;
    public float DashDuration;
    private bool IsDash = false;
    public Vector3 Velocity;
    // Start is called before the first frame update
    void Start()
    {
        
        _controller = GetComponent<CharacterController>();
        
    }
    [Header("Variables movimiento")]
    [SerializeField]
    private float _gravity;
    [SerializeField]
    private float _playerSpeed;
    [SerializeField]
    private float _jumpHeight;
    [SerializeField]
    private float _yVelocity;
    public float Dashforce;
    

    
    
    // Update is called once per frame
    void Update()
    {
        ActualTime += Time.deltaTime;
        float HorizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(HorizontalInput, 0, 0);

        if (Input.GetButtonDown("Dash"))

        {
            IsDash = true;
            ActualTime = 0;

        }
        
        if (IsDash == true && ActualTime < DashDuration)
        {
            _playerSpeed = Dashforce;
        }
        else if ( ActualTime >= DashDuration)
        {
            ActualTime = 0;
            _playerSpeed = 20;
            IsDash = false;
        }
        
        

        Velocity = direction * _playerSpeed;



        if (_controller.isGrounded == true)
        {
            Debug.Log("Player is grounded.");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
            }
        }
        else
        {
            _yVelocity -= _gravity;
        }
        Velocity.y = _yVelocity;


        _controller.Move(Velocity * Time.deltaTime);


    }
}
