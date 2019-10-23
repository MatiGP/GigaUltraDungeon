using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponDamage : MonoBehaviour
{
    public Weapon_SO weapon;

    private int minDamage;
    private int maxDamage;
    private int randomDamage;

    private void LateUpdate()
    {
        if (weapon)
        {
            minDamage = weapon.weaponMinDamage;
            maxDamage = weapon.weaponMaxDamage;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            randomDamage = Random.Range(minDamage, maxDamage);
            collision.gameObject.GetComponent<EnemyScript>().TakeDamage(randomDamage);
        }
    }
}
