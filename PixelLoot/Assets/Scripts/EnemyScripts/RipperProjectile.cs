using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RipperProjectile : EnemyProjectile
{
    
    public float maxThrowDistance = 5f;

    private Transform ripperPos;
    private int collisionCount = 0;
    private Vector2 throwStartPos;
    private bool returnProjectile;

    // Start is called before the first frame update
    void Start()
    {
        
        throwStartPos = ripperPos.position;
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!returnProjectile)
        {
            if(Vector2.Distance(throwStartPos, transform.position) < maxThrowDistance)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
            else
            {
                returnProjectile = true;
            }
        }
        else
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStats>().TakeDamage(damage);
            Instantiate(onHitParticle, transform.position, Quaternion.identity);
            collisionCount++;
            if(collisionCount >= 2)
            {
                DestroyProjectile();
            }
        }
        if (collision.CompareTag("Wall"))
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Instantiate(onHitParticle, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }  
    public void SetRipperPos(Transform pos)
    {
        ripperPos = pos;
    }
}
