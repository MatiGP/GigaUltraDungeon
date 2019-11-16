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


    public Transform playerPos;
    private int enemyMaxHealth;
    private int enemyCurrentHealth;

    private void Start()
    {
        enemyMaxHealth = enemyTemplate.enemyHealth;
        enemyCurrentHealth = enemyMaxHealth;
    }

    private void Update()
    {
        Collider2D targetInViewRadius = Physics2D.OverlapCircle(transform.position, 7f, playerLayer);
        if(targetInViewRadius != null)
        {
            if (!Physics2D.Raycast(transform.position, (targetInViewRadius.transform.position - transform.position).normalized, 7f, obstacleLayer))
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
