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

    public GameObject[] drop;


    private Transform playerPos;
    private int enemyMaxHealth;
    private int enemyCurrentHealth;

    private void Start()
    {
        enemyMaxHealth = enemyTemplate.enemyHealth;
        enemyCurrentHealth = enemyMaxHealth;
    }

    private void FixedUpdate()
    {
        Collider2D targetInViewRadius = Physics2D.OverlapCircle(transform.position, 7f, playerLayer);
        if(targetInViewRadius != null)
        {
            playerPos = targetInViewRadius.transform;
            if (!Physics2D.Linecast(transform.position, playerPos.position, obstacleLayer))
            {
                
                Debug.Log(targetInViewRadius.name);
            }
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
