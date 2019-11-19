using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage;
    public ParticleSystem onHitParticle;
    public float lifeTime;
    public int speed = 10;

    // Update is called once per frame
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
