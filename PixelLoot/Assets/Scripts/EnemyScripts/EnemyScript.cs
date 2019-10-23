using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public Enemy_SO enemyTemplate;
    public Image enemyHealthBar;

    private int enemyCurrentHealth;
    private int enemyMaxHealth;

    private void Start()
    {
        enemyMaxHealth = enemyTemplate.enemyHealth;
        enemyCurrentHealth = enemyMaxHealth;
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

}
