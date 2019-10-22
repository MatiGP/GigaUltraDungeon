using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        scale = PlayerStats.instance.GetComponent<Transform>().localScale;
        transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * scale.x * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyScript>().health -= 3;
            Destroy(gameObject);
        }
    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
