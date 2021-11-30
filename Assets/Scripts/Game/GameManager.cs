using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject charachter;
    public GameObject spawn;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;
    public GameObject pause;
    public GameObject settings;
    public bool paused = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Respawn();
        if (Input.GetButtonDown("Pause"))
        {
            CloseSettings();
            Pause();
        }

        if(paused == true)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void Pause()
    {
        if (paused == false)
        {
            pause.SetActive(true);
            paused = true;
        }
        else if (paused == true)
        {
            pause.SetActive(false);
            paused = false;
        }
    }


    private void Respawn()
    {
        if (player.lifes <= 0)
        {
            player.rb.transform.position = spawn.transform.position;
            player.lifes = player.maxlifes;
            if (player.checkpoint1 == true)
            {
                player.rb.transform.position = spawn2.transform.position;
                player.lifes = player.maxlifes; 
            }
        }
    }
    public void OpenSettings()
    {
        settings.SetActive(true);
        pause.SetActive(false);
    }
    public void CloseSettings()
    {
        settings.SetActive(false);
        pause.SetActive(true);
    }
}
