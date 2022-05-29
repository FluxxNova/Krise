using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public float radius;
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
    public float timeToAttack;
    public float spawnTime;
    public float attackTime;
    public NavMeshAgent agent;
    public NewPlayerMovement player;
    public Transform target;
    public Animator animator;
    public int attackNum = 1;
    public GameObject fire;
    public float fireSpawn;
    public Image lifebar;
    private Collider coll;


    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        gameManager = FindObjectOfType<GameManager>();
        renderers = GetComponentsInChildren<Renderer>();
        player = FindObjectOfType<NewPlayerMovement>();
        target = player.transform;
        coll = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        timeToAttack += Time.deltaTime;
        spawnTime += Time.deltaTime;
        int collisions = Physics.OverlapSphereNonAlloc(transform.position, radius, hits, playerLayer);

        if (collisions > 0)
        {
            targetDetected = true;
            playerTransform = hits[0].transform;
        }

        if (agent.speed > 1)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        if (targetDetected)
        {
            radius = 100;
            if (timeToAttack > attackTime && attackNum == 1)
            {
                animator.SetBool("Attack", true);
                StartCoroutine(Attack());
            }
            else if (timeToAttack < attackTime)
            {
                agent.speed = 0.1f;
                agent.SetDestination(target.position);
            }
            if (timeToAttack > attackTime && attackNum == 2)
            {
                animator.SetBool("Shot", true);
                StartCoroutine(ShotFire());
            }
        }

    }
    
    public void GetDamage()  // Muerte del enemigo
    {
        life--;
        lifebar.fillAmount -= 0.02f;
        Debug.Log("Ouch");

        if (life <= 0)
            gameManager.Win();     
        
        if (life == 25)
        {
            animator.SetBool("Spawn", true);
            StartCoroutine(spawnEnemies());
        }
    }
    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
        coll.enabled = false;
        Debug.Log("elpepe");
        agent.speed = 150f;
        agent.SetDestination(target.position);
        yield return new WaitForSeconds(0.7f);
        animator.SetBool("Attack", false);
        Debug.Log("seacabo");
        agent.speed = 0.1f;
        timeToAttack = 0;
        attackNum = 2;
        coll.enabled = true;
        yield return new WaitForSeconds(2f);
    }
    public IEnumerator ShotFire()
    {
        yield return new WaitForSeconds(0f);
        timeToAttack = 0;
        attackNum = 1;
        animator.SetBool("Shot", false);
        yield return new WaitForSeconds(1f);
        shotFire();
    }
    public IEnumerator spawnEnemies()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(golem, player.transform.position + new Vector3(-3, 1), Quaternion.Euler(0, 90, 0));
        yield return new WaitForSeconds(2f);
        Instantiate(limus, player.transform.position + new Vector3(-10, 1), Quaternion.Euler(270, 90, 0));
        yield return new WaitForSeconds(2f);
        Instantiate(fatum, player.transform.position + new Vector3(-15, 2.7f), Quaternion.Euler(0, 90, 0));
        animator.SetBool("Spawn", false);
    }
    public void shotFire()
    {
        Instantiate(fire, this.transform.position + new Vector3(0, fireSpawn), Quaternion.Euler(0, -90, 0));
        Instantiate(fire, this.transform.position + new Vector3(5, fireSpawn), Quaternion.Euler(0, -90, 0));
        Instantiate(fire, this.transform.position + new Vector3(-5, fireSpawn), Quaternion.Euler(0, -90, 0));
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            targetDetected = true;
        }
        //targetDetected = false;
    }

}
