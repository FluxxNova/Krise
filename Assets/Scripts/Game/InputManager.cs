using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager: MonoBehaviour
{
    private Player player;
    public GodMode godMode;
    private Fires fires;
    private float axisy = 0f;
    private float axisx = 0f;
    Playercontrols controls;
    Vector2 move;

    // Start is called before the first frame update

    private void Awake()
    {
        controls = new Playercontrols();
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        godMode = FindObjectOfType<GodMode>();
        fires = FindObjectOfType<Fires>();
    }

    // Update is called once per frame
    void Update()
    {
        move.x = axisx;
        move.y = axisy;


        //player.MovePlayerY(axisy);
        //player.MovePlayerX(axisx);

        /*if (Input.GetButtonDown("Jump"))
            player.Jump();

        if (Input.GetButtonDown("Dash"))
            player.Dash();

        if (Input.GetButtonDown("GodMode"))
            godMode.ActiveGodMenu();

        if (Input.GetButtonDown("Camperola"))
            fires.Spawn();

        if (Input.GetButtonDown("Fire1"))
            player.Attack();*/

    }

}
