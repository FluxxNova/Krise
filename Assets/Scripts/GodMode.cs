using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodMode : MonoBehaviour
{
    public Player player;
    public GameObject menu;
    public bool isInmortal;
    public bool canFly;
    private bool menuActive;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ActiveGodMenu()
    {
        if (menuActive == false)
        {
            menu.SetActive(true);
            menuActive = true;
        }
        else if (menuActive == true)
        {
            menu.SetActive(false);
            menuActive = false;
        }
    }

    public void Fly()
    {
        canFly = !canFly;

        if (canFly == true)
        {
           player.rb.isKinematic = true;
        }
        else if (canFly == false)
        {
            player.rb.isKinematic = false;
        }

    }
}
