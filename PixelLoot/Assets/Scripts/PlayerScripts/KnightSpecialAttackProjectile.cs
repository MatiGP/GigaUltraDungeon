using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSpecialAttackProjectile : MonoBehaviour
{

    public int damage;
    public ParticleSystem onHitParticle;
    public float lifeTime;
    public int speed = 15;

    // Update is called once per frame
    private void Start()
    {     
        Invoke("DestroyProjectile", lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Instantiate(onHitParticle, transform.position, Quaternion.identity);
        }

        if (collision.CompareTag("Wall"))
        {
            DestroyProjectile();
        }


    }



    public void DestroyProjectile()
    {
        Instantiate(onHitParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
