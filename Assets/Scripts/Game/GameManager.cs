using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public NewPlayerMovement player;
    public Mixer mixer;
    public GameObject charachter;
    public GameObject spawn;
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject pause;
    public GameObject settings;
    public GameObject graphics;
    public GameObject audio;
    public GameObject controls;
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
        int index = Random.Range(4, 7);
        Cursor.visible = true;
        SceneManager.LoadScene(index);
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

    public void AudioButton()
    {
        audio.SetActive(true);
        graphics.SetActive(false);
        controls.SetActive(false);
    }
        
    public void ControlsButton()
    {
        audio.SetActive(false);
        graphics.SetActive(false);
        controls.SetActive(true);
    }
        
    public void GraphicsButton()
    {
        audio.SetActive(false);
        graphics.SetActive(true);
        controls.SetActive(false);
    }

}
