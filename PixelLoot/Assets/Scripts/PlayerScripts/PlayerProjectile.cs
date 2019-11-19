using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [HideInInspector]
    public int damage;
    public ParticleSystem onHitParticle;
    public float lifeTime;
    public int speed = 15;

    // Update is called once per frame
    private void Start()
    {
        damage = Inventory.instance.weaponHolder.GetComponent<WeaponDamage>().CalculateRandomWeaponDamage();
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
            DestroyProjectile();
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
