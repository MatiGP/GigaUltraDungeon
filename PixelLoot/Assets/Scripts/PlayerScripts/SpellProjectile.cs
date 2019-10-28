using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public GameObject particle;


    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);        
    }

    void DestroyProjectile()
    {
        Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyScript>().TakeDamage(2);
            Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
