using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiablikProjectile : EnemyProjectile
{

    public GameObject diablikSmallProjectiles;
    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }


    void DestroyProjectile()
    {
        Instantiate(onHitParticle, transform.position, Quaternion.identity);
        for(int i = 0; i<4; i++)
        {
            Instantiate(diablikSmallProjectiles, transform.position, Quaternion.Euler(0,0, i*90));
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStats>().TakeDamage(damage);
            DestroyProjectile();
        }
        if (collision.CompareTag("Wall"))
        {
            DestroyProjectile();
        }

    }
}
