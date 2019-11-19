using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public Enemy_SO enemyTemplate;
    public Image enemyHealthBar;
    public float tauntRange = 8f;
    public float attackRange = 4f;

    public GameObject[] drop;

    public float nextWaypointDistance = 1.2f;

    [HideInInspector]
    public LayerMask playerLayer;
    [HideInInspector]
    public LayerMask obstacleLayer;
    private Animator animator;
    private Rigidbody2D enemyRigidbody;
    private bool isTaunted;
    public Transform playerPos;
    private Seeker seeker;
    private int enemyMaxHealth;
    private int enemyCurrentHealth;
    private Path path;
    private int currentWaypoint = 0;

    private void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyMaxHealth = enemyTemplate.enemyHealth;
        enemyCurrentHealth = enemyMaxHealth;
        seeker = GetComponent<Seeker>();
        animator = GetComponent<Animator>();

    }

    private void FixedUpdate()
    {
        if (!isTaunted)
        {
            Collider2D targetInViewRadius = Physics2D.OverlapCircle(transform.position, tauntRange, playerLayer);
            if (targetInViewRadius != null)
            {
                playerPos = targetInViewRadius.transform;
                if (!Physics2D.Linecast(transform.position, playerPos.position, obstacleLayer))
                {
                    isTaunted = true;

                    InvokeRepeating("UpdatePath", 0f, 0.5f);
                    animator.SetBool("isRunning", true);
                }
            }
        }
        
    }

    void UpdatePath()
    {
        if(playerPos != null)
        {
            seeker.StartPath(transform.position, playerPos.position, OnPathComplete);
        }       
        if (seeker.IsDone())
        {
            
            if(transform.position.x > playerPos.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
    
    private void Update()
    {
        if (isTaunted)
        {

            if (path == null)
            {
                return;
            }

            if (currentWaypoint >= path.vectorPath.Count)
            {
                return;
            }
            if (playerPos == null)
            {
                isTaunted = false;
                animator.SetBool("isRunning", false);
                return;
            }

            Vector2 direction = ((Vector2)(path.vectorPath[currentWaypoint] - transform.position)).normalized;

            
            if(Vector2.Distance(transform.position, playerPos.position) > attackRange)
            {
                enemyRigidbody.velocity = direction * enemyTemplate.enemySpeed;
            }
            else
            {
                animator.SetTrigger("attack");
            }
           
            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);           

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }
    }
    
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        enemyCurrentHealth -= damage;
        if(enemyCurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

   

   
}
