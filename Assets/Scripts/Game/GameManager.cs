using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public Player player;
    public Mixer mixer;
    public GameObject charachter;
    public GameObject spawn;
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject pause;
    public GameObject settings;
    public bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        Object.DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        

        
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
            mixer.paused.TransitionTo(0);
        }
        else if (paused == true)
        {
            pause.SetActive(false);
            paused = false;
            mixer.unpaused.TransitionTo(0);

        }
    }


    public void Die()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Win()
    {
        SceneManager.LoadScene("WinScene");
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
