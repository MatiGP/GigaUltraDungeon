using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpellWithStaff : MonoBehaviour
{
    public GameObject projectile;
    public Vector3 offset;

    private float timeBtwCasts;
    public float startTimeBtwCasts;


    private void Update()
    {
        if(projectile == null && GetComponent<MeleeWeaponDamage>().weapon != null)
        {
            projectile = GetComponent<MeleeWeaponDamage>().weapon.projectileIfPossible;
        }
        if(timeBtwCasts <= 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if(PlayerStats.instance.characterCurrentMana > 0)
                {
                    PlayerStats.instance.SpendMana(2);
                    Instantiate(projectile, transform.parent.position + offset, Quaternion.identity);
                    timeBtwCasts = startTimeBtwCasts;
                }
                

            }
        }
        else
        {
            timeBtwCasts -= Time.deltaTime;
        }
    }
}
