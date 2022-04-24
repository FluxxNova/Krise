using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Player player;
    public Mixer mixer;
    public GameObject charachter;
    public GameObject spawn;
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject pause;
    public GameObject settings;
    public GameObject hud;
    public bool paused = false;
    NewPlayerMovement newPlayer;
    // Start is called before the first frame update
    void Start()
    {
        //Object.DontDestroyOnLoad(this.gameObject);        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pause()
    {
        if (paused == false)
        {
            pause.SetActive(true);
            hud.SetActive(false);
            paused = true;
            mixer.paused.TransitionTo(0);
            Time.timeScale = 0;
            Cursor.visible = true;
        }
        else if (paused == true)
        {
            pause.SetActive(false);
            hud.SetActive(true);
            paused = false;
            mixer.unpaused.TransitionTo(0);
            Time.timeScale = 1;
            Cursor.visible = false;
        }
    }


    public void Die()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("GameOver");
        //SceneManager.LoadScene("GameOver", LoadSceneMode.Additive); //Sobreponer la escena de gameover encima del gameplay para luego quitarla
        //player.isDead = true;
    }

    public void Win()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("WinScene");
    }

    
    public void OpenSettings()
    {
        player.audioManager.PlayClip(2);
        settings.SetActive(true);
        pause.SetActive(false);
    }
    public void CloseSettings()
    {
        settings.SetActive(false);
        pause.SetActive(true);
    }

}
