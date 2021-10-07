using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    #region Declaraciones
    public enum EnemyState { Idle, Patrol, Chase, Explode }
    public EnemyState state;
    private NavMeshAgent agent;
    public Transform targetTransform;
    public float speed = 10f;
    public int life = 3;

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

    #endregion

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
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
        if (targetDetected && state != EnemyState.Chase && state != EnemyState.Explode)
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
    }

    #region Sets 

    private void SetIdle()
    {
        state = EnemyState.Idle;
        agent.isStopped = true;
        radius = radiusIdle;

    }

    private void SetPatrol()
    {
        state = EnemyState.Patrol;
        agent.isStopped = false;
        agent.SetDestination(targetTransform.position);
    }

    private void SetChase()
    {
        state = EnemyState.Chase;
        agent.isStopped = false;
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
        if (agent.remainingDistance <= 0.5f)
        {
            SetIdle();
        }
    }

    private void ChaseUpdate()
    {
        agent.SetDestination(targetTransform.position);

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

        targetTransform = nodes[currentNode];
    }
    
    public void GetDamage()
    {
        life--;

        Debug.Log("Ouch");

        if (life <= 0)
            Destroy(gameObject);
        StartCoroutine(ColorAnimation());
        GameObject ps = Instantiate(bloodGO, transform.position, Quaternion.identity);
        Destroy(ps, 3f);
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

    private IEnumerator ColorAnimation()
    {
        ChangeColor(Color.red);
        yield return new WaitForSeconds(0.5f);
        ChangeColor(Color.white);
    }

    private void OnDrawGizmos()
    {
        Color gizmoColor = Color.red;
        gizmoColor.a = 0.5f;
    }
}
