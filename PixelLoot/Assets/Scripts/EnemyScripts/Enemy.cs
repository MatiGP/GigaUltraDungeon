using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public Enemy_SO enemyTemplate;
    public Image enemyHealthBar;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;
    public float nextWaypointDistance = 1.2f;

    public GameObject[] drop;

    private Rigidbody2D enemyRigidbody;
    private bool isTaunted;
    private Transform playerPos;
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



        
    }

    private void FixedUpdate()
    {
        if (!isTaunted)
        {
            Collider2D targetInViewRadius = Physics2D.OverlapCircle(transform.position, 7f, playerLayer);
            if (targetInViewRadius != null)
            {
                playerPos = targetInViewRadius.transform;
                if (!Physics2D.Linecast(transform.position, playerPos.position, obstacleLayer))
                {
                    isTaunted = true;
                    
                }
            }
        }       
    }
    private void Update()
    {
        if (isTaunted)
        {
            seeker.StartPath(transform.position, playerPos.position, OnPathComplete);

            if (path == null)
            {
                return;
            }

            if (currentWaypoint >= path.vectorPath.Count)
            {
                return;
            }

            Vector2 direction = ((Vector2)(path.vectorPath[currentWaypoint] - transform.position)).normalized;
            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

            enemyRigidbody.velocity = direction;

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
