using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : PlayerProjectile
{
    

    public override void DestroyProjectile()
    {
        base.DestroyProjectile();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }
}
