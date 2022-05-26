using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Titania : MonoBehaviour
{
    #region Declaraciones
    public Transform targetTransform;
    public float speed = 10f;
    public int life = 3;
    public Rigidbody rb;


    [Header("Chase")]
    public float radiusIdle = 5;
    public float radiusChase = 10f;
    private float radius;
    public LayerMask playerLayer;
    public bool targetDetected;
    private Transform playerTransform;
    private Collider[] hits = new Collider[10];

    [Space]
    public GameManager gameManager;
    private Renderer[] renderers;
    private MaterialPropertyBlock materialProperty;
    public GameObject bloodGO;
    public GameObject fatum;
    public GameObject limus;
    public GameObject golem;
    public float timeToSpawn;
    public float spawnTime;

    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    private void FixedUpdate()
    {
        timeToSpawn += Time.deltaTime;
        int collisions = Physics.OverlapSphereNonAlloc(transform.position, radius, hits, playerLayer);

        if (collisions > 0)
        {
            targetDetected = true;
            playerTransform = hits[0].transform;
        }

        if (targetDetected)
        {
            for (int i = 0; i < 1; i++)
                StartCoroutine(SpawnEnemies());
            //targetDetected = false;
            if (timeToSpawn >= 5)
            {
                timeToSpawn = 0f;
                targetDetected = false;
            }
        }

    }
    
    public void GetDamage()  // Muerte del enemigo
    {
        life--;

        Debug.Log("Ouch");

        if (life <= 0)
            gameManager.Win();
        StartCoroutine(ColorAnimation());
        
    }

    private void ChangeColor(Color color)
    {
        materialProperty = new MaterialPropertyBlock();

        materialProperty.SetColor("_Color", color);
        
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].SetPropertyBlock(materialProperty);
        }
    }

    
    private IEnumerator SpawnEnemies()
    {
        if (timeToSpawn >= spawnTime)
        {
            yield return new WaitForSeconds(2f);
            Instantiate(golem, this.transform.position + new Vector3(-3, 1), Quaternion.Euler(0, -90, 0));
            yield return new WaitForSeconds(2f);
            Instantiate(limus, this.transform.position + new Vector3(-10, 1), Quaternion.Euler(270, 270, 0));
            yield return new WaitForSeconds(2f);
            Instantiate(fatum, this.transform.position + new Vector3(-15, 2.7f), Quaternion.Euler(0, -90, 0));
        }

    }

    private IEnumerator ColorAnimation()
    {
        ChangeColor(Color.red);
        yield return new WaitForSeconds(0.5f);
        ChangeColor(Color.white);
    }
    private void OnTriggerEnter(Collider collision)
    {

        targetDetected = true;
        targetDetected = false;
    }

}
