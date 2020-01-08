using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcherSpecialAttack : MonoBehaviour
{
    public int manaCost;
    public GameObject specialProjectile;
    public float specialAttackCooldown;
    public Image skillno2;
    private float currentSpecialAttackCooldown;
    private ShootProjectile shootProjectile;
    // Update is called once per frame

    private void Start()
    {
        shootProjectile = GetComponent<ShootProjectile>();
    }
    void Update()
    {
        if (currentSpecialAttackCooldown <= 0 && PlayerStats.instance.GetCurrentMana() >= manaCost)
        {
            skillno2.fillAmount = 1;
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
            skillno2.fillAmount = currentSpecialAttackCooldown / specialAttackCooldown;
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
