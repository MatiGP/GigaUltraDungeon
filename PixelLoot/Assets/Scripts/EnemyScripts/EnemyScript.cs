using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public CasterEnemy_SO character;
    public Image enemyHealthBar;
    [HideInInspector]
    public Animator animator;
    public GameObject deathParticle;
    private int characterCurrentHealth;
    private int characterMaxHealth;
    public List<GameObject> drop = new List<GameObject>();


    private void Start()
    {
        characterMaxHealth = character.enemyHealth;
        characterCurrentHealth = characterMaxHealth;
        animator = GetComponent<Animator>();
    }

    void UpdateUI()
    {
        enemyHealthBar.fillAmount = (float)characterCurrentHealth / characterMaxHealth;
    }

    public void Attack()
    {
        Instantiate(character.projectile, transform.position, Quaternion.identity);
    }

    public void TakeDamage(int damage)
    {
        characterCurrentHealth -= damage;
        animator.SetBool("playerSpotted", true);       
    }

    private void Update()
    {
        UpdateUI();
        if (characterCurrentHealth <= 0)
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Instantiate(drop[Random.Range(0, drop.Count - 1)], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    



}
