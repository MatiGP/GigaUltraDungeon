using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour
{
    public GameObject projectile;
    public Vector3 offset;

    private float timeBtwShots;
    public float startTimeBtwShots;  
    
    void Update()
    {
        if (projectile == null && GetComponent<MeleeWeaponDamage>().weapon != null)
        {
            projectile = GetComponent<MeleeWeaponDamage>().weapon.projectileIfPossible;
        }
        if (timeBtwShots <= 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Instantiate(projectile, transform.parent.position + offset, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
            }            
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
