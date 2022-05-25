using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fatum : MonoBehaviour
{
    #region Declaraciones
    public enum EnemyState { Idle, Patrol, Chase}
    public EnemyState state;
    //private NavMeshAgent agent;
    public Transform targetTransform;
    public float speed = 10f;
    public int life = 3;
    public Rigidbody rb;

    [Header("Patorl")]
    public Transform[] nodes;
    private int currentNode;

    [Header("Idle")]
    public float idleTime = 2f;
    private float currentTime;

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
    public GameObject fire;
    public float fireSpawn = 0;
    public Animator animator;

    #endregion

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        //agent = GetComponent<NavMeshAgent>();
        //agent.speed = speed;
        gameManager = FindObjectOfType<GameManager>();
        SetIdle();
        renderers = GetComponentsInChildren<Renderer>();
    }
    void Update()
    {
        switch (state)
        {
            case EnemyState.Idle:
                {
                    IdleUpdate();
                    break;
                }
            case EnemyState.Patrol:
                {
                    PatrolUpdate();
                    break;
                }
            case EnemyState.Chase:
                {
                    ChaseUpdate();
                    break;
                }
        }
        if (targetDetected && state != EnemyState.Chase )
            SetChase();

    }

    private void FixedUpdate()
    {
        if (targetDetected)
            return;

        int collisions = Physics.OverlapSphereNonAlloc(transform.position, radius, hits, playerLayer);

        if (collisions > 0)
        {
            targetDetected = true;
            playerTransform = hits[0].transform;
        }
        if (targetDetected)
        {
            StartCoroutine(ShotFire());
            Debug.Log("elpepepepepe");
        }
    }

    #region Sets 

    private void SetIdle()
    {
        state = EnemyState.Idle;
        //agent.isStopped = true;
        radius = radiusIdle;

    }

    private void SetPatrol()
    {
        state = EnemyState.Patrol;
        //agent.isStopped = false;
        //agent.SetDestination(targetTransform.position);
    }

    private void SetChase()
    {
        state = EnemyState.Chase;
        //agent.isStopped = false;
        radius = radiusChase;
        targetTransform = playerTransform;
    }
    #endregion

    #region Updates
    private void IdleUpdate()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= idleTime)
        {
            currentTime = 0;
            GoNextNode();
            SetPatrol();
        }
    }

    private void PatrolUpdate()
    {
        /*if (agent.remainingDistance <= 0.5f)
        {
            SetIdle();
        }*/
    }

    private void ChaseUpdate()
    {
        //agent.SetDestination(targetTransform.position);

        float distance = Vector3.Distance(transform.position, targetTransform.position);

        if (distance >= radius)
        {
            targetDetected = false;
            SetIdle();
        }
        
    }
    #endregion

    private void GoNextNode()
    {
        currentNode++;

        if (currentNode >= nodes.Length)
            currentNode = 0;

        //targetTransform = nodes[currentNode];
    }
    
    public void GetDamage()  // Muerte del enemigo
    {
        life--;

        Debug.Log("Ouch");

        if (life <= 0)
            Destroy(gameObject);
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

    public void Fire()
    {
        StartCoroutine(ShotFire());
    }

    public IEnumerator ShotFire()
    {
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("attack");
        Instantiate(fire, this.transform.position + new Vector3(0, fireSpawn), Quaternion.Euler(0, -90, 0));
        yield return new WaitForSeconds(2f);
    }

    private IEnumerator ColorAnimation()
    {
        ChangeColor(Color.red);
        yield return new WaitForSeconds(0.5f);
        ChangeColor(Color.white);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerAttack")
        {
            GetDamage();
        }

    }
}
