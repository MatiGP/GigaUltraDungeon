using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [HideInInspector]
    public int damage;
    public ParticleSystem onHitParticle;
    public float lifeTime;

    // Update is called once per frame
    private void Start()
    {
        damage = Inventory.instance.weaponHolder.GetComponent<WeaponDamage>().CalculateRandomWeaponDamage();
        Invoke("DestroyProjectile", lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * 15 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyScript>().TakeDamage(damage);
            DestroyProjectile();
        }
        if (collision.CompareTag("Wall"))
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Instantiate(onHitParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
