using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectile;
    public float offset = -90;
    public float startTimeBtwShots = 1f;
    private PlayerController controller;
    private float timeBtwShots;
    private float rotz;

    private void Start()
    {
        controller = GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if (timeBtwShots <= 0 && projectile)
        {
            if (Input.GetMouseButtonDown(0) && controller.canAttack)
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

    public float GetRotation()
    {
        return rotz + offset;
    }
}
