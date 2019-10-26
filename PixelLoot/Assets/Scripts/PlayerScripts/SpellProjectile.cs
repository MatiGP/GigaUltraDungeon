﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;


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
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyScript>().TakeDamage(2);
            Destroy(gameObject);
        }
    }
}