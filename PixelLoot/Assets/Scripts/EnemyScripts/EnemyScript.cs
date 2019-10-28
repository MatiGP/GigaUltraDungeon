using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public CasterEnemy_SO enemyTemplate;
    public Image enemyHealthBar;
    [HideInInspector]
    public Animator animator;
    public GameObject deathParticle;
    public List<GameObject> drop = new List<GameObject>();

    private int enemyCurrentHealth;
    private int enemyMaxHealth;

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
        animator.SetBool("playerSpotted", true);      
        
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
        if (enemyCurrentHealth <= 0)
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Instantiate(drop[Random.Range(0, drop.Count - 1)], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
 

}
