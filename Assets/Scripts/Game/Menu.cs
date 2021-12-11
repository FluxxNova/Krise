using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private float transitionTime = 1f;
    private Player player;
    public Animator fadeAnimator;
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
        StartCoroutine(SceneLoadGameplay());
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
        StartCoroutine(SceneLoadMenu());
    }
    



    public void CloseGame()
    {
        Application.Quit();
    }

    public IEnumerator SceneLoadGameplay()
    {
        fadeAnimator.SetTrigger("StartTransition");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("Gameplay");
    }


    public IEnumerator SceneLoadMenu()
    {
        fadeAnimator.SetTrigger("StartTransition");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("MenuPrincipal");
    }

}
