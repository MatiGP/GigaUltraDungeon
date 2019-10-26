using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public bool playerSpotted;
    public CasterEnemy_SO enemyTemplate;
    public Image enemyHealthBar;
    

    private int enemyCurrentHealth;
    private int enemyMaxHealth;
    private Animator animator;

    private void Start()
    {
        enemyMaxHealth = enemyTemplate.enemyHealth;
        enemyCurrentHealth = enemyMaxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        enemyCurrentHealth -= damage;
        UpdateUI();
        if(enemyCurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
        
    }

    void UpdateUI()
    {
        enemyHealthBar.fillAmount = (float)enemyCurrentHealth / enemyMaxHealth;
    }

    public void Attack()
    {
        Instantiate(enemyTemplate.projectile, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        if (playerSpotted)
        {
            animator.SetBool("playerSpotted", true);
        }
        else
        {
            animator.SetBool("playerSpotted", false);
        }
    }
}
