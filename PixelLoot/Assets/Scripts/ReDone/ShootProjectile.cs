using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectile;
    public float offset = -90;
    public float startTimeBtwShots = 1f;
    
    private float timeBtwShots;
    private float rotz;

    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, rotz + offset);

        if (projectile == null && GetComponent<MeleeWeaponDamage>().weapon != null)
        {
            projectile = GetComponent<MeleeWeaponDamage>().weapon.projectileIfPossible;
        }

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void Shoot()
    {   
        Instantiate(projectile, transform.position, Quaternion.Euler(0f, 0f, rotz + offset));
        timeBtwShots = startTimeBtwShots;
    }
}
