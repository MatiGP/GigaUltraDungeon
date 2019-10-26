using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpellWithStaff : MonoBehaviour
{
    public GameObject projectile;
    public float offset = -90;

    private float timeBtwCasts;
    public float startTimeBtwCasts;


    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotz + offset);
        
        if(projectile == null && GetComponent<MeleeWeaponDamage>().weapon != null)
        {
            projectile = GetComponent<MeleeWeaponDamage>().weapon.projectileIfPossible;
        }


        if(timeBtwCasts <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(PlayerStats.instance.characterCurrentMana > 0)
                {
                    PlayerStats.instance.SpendMana(2);
                    Instantiate(projectile, transform.position, Quaternion.Euler(0f, 0f, rotz + offset));
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
