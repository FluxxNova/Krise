using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager1: MonoBehaviour
{
    private Player player; 
    private float axisy = 0f;
    private float axisx = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        axisy = Input.GetAxis("Vertical");
        player.MovePlayerY(axisy);

        axisx = Input.GetAxis("Horizontal");
        player.MovePlayerX(axisx);

        if (Input.GetButtonDown("Jump"))
            player.Jump();

        if (Input.GetButtonDown("Dash"))
            player.Dash();

    }
}
