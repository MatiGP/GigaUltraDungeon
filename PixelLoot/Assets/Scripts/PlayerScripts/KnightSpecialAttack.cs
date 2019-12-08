using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnightSpecialAttack : MonoBehaviour
{
    public int manaCost;
    public GameObject specialProjectile;
    public float specialAttackCooldown;
    public Image skillno2;
    private float currentSpecialAttackCooldown;
    private ShootProjectile shootProjectile;
    [SerializeField]
    private PlayerController controller;
    // Update is called once per frame

    private void Start()
    {
        shootProjectile = GetComponent<ShootProjectile>();
    }
    void Update()
    {
        if (currentSpecialAttackCooldown <= 0 && PlayerStats.instance.characterCurrentMana >= manaCost)
        {
            skillno2.fillAmount = 1;
            if (Input.GetKeyDown(KeyCode.Mouse1) && controller.canCastSpells)
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
        Instantiate(specialProjectile, transform.position, Quaternion.Euler(0f, 0f, shootProjectile.GetRotation()));
        currentSpecialAttackCooldown = specialAttackCooldown;
    }
}
