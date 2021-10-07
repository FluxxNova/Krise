using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager1: MonoBehaviour
{
    private Player player; 
    private float axis = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        axis = Input.GetAxis("Horizontal");
        player.MovePlayer(axis);

        if (Input.GetButtonDown("Jump"))
            player.Jump();

        if (Input.GetButtonDown("Dash"))
            player.Dash();

    }
}
