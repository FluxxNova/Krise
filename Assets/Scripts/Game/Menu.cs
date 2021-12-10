using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    private Player player;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGameplayScene()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void Respawn()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("Settings");
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    

    public void CloseGame()
    {
        Application.Quit();
    }

}
