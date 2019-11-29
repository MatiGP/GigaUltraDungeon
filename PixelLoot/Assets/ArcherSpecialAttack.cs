using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherSpecialAttack : MonoBehaviour
{
    public int manaCost;
    public GameObject specialProjectile;
    public float specialAttackCooldown;
    private float currentSpecialAttackCooldown;
    private ShootProjectile shootProjectile;
    // Update is called once per frame

    private void Start()
    {
        shootProjectile = GetComponent<ShootProjectile>();
    }
    void Update()
    {
        if (currentSpecialAttackCooldown <= 0 && PlayerStats.instance.characterCurrentMana >= manaCost)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                PlayerStats.instance.SpendMana(manaCost);
                ShootSpecial();
                currentSpecialAttackCooldown = specialAttackCooldown;
            }
        }
        else
        {
            currentSpecialAttackCooldown -= Time.deltaTime;
        }

    }

    void ShootSpecial()
    {
        for(int i = 0; i <= 6; i++)
        {
            Instantiate(specialProjectile, transform.position, Quaternion.Euler(0f, 0f, shootProjectile.GetRotation() - (30 - i * 10)));
        }
        currentSpecialAttackCooldown = specialAttackCooldown;
    }
}
