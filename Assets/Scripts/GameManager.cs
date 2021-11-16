using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject limus;
    public GameObject fatum;
    public GameObject golem;
    public GameObject spawn;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
       
            
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
}