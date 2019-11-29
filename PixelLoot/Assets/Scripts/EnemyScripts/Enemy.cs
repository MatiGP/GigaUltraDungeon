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
    public ParticleSystem deathParticle;

    public GameObject[] drop;

    public float nextWaypointDistance = 1.2f;

    
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;
    private Animator animator;
    private Rigidbody2D enemyRigidbody;
    private bool isTaunted;
    [HideInInspector]
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
        else
        {
            return;
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
                animator.SetBool("isInRange", false);
                return;
            }

            Vector2 direction = ((Vector2)(path.vectorPath[currentWaypoint] - transform.position)).normalized;

            
            if(Vector2.Distance(transform.position, playerPos.position) > attackRange)
            {
                enemyRigidbody.velocity = direction * enemyTemplate.enemySpeed;
                animator.SetBool("isInRange", false);
                animator.ResetTrigger("attack");
                if (transform.position.x > playerPos.position.x)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            else
            {
                animator.SetTrigger("attack");
                animator.SetBool("isInRange", true);
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
        enemyHealthBar.fillAmount = (float)enemyCurrentHealth / enemyMaxHealth;
        if(enemyCurrentHealth <= 0)
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            DropItem();
            Destroy(gameObject);
        }
    }

    void DropItem()
    {
        int rand = Random.Range(1, 100);

        if(rand > 70)
        {
            Instantiate(drop[0], transform.position, Quaternion.identity);
        }
    }

    




}
