using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager: MonoBehaviour
{
    private Player player;
    public GodMode godMode;
    private Fires fires;
    private float axisy = 0f;
    private float axisx = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        godMode = FindObjectOfType<GodMode>();
        fires = FindObjectOfType<Fires>();
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

        if (Input.GetButtonDown("GodMode"))
            godMode.ActiveGodMenu();

        if (Input.GetButtonDown("Camperola"))
            fires.Spawn();

    }
}
