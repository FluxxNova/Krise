using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mando : MonoBehaviour
{
    Playercontrols controls;

    Vector2 move;
    
    // Start is called before the first frame update
    void Start()
    {
        controls = new Playercontrols();
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 m = new Vector2(-move.x, move.y) * Time.deltaTime;
        transform.Translate(m, Space.World);
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
