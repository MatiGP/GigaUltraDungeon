using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    Vector3 scale;

    private void Start()
    {
        scale = PlayerStats.instance.GetComponent<Transform>().localScale;
        transform.localScale = scale;
        Invoke("DestroyProjectile", lifeTime);
    }

    private void Update()
    {
        transform.Translate(transform.right * scale.x * speed * Time.deltaTime);        
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyScript>().health -= 5;
            Destroy(gameObject);
        }
    }
}
