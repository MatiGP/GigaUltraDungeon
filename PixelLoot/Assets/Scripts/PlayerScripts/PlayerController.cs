using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    public int moveSpeed;


    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Vertical"), 0);
        spriteRenderer.flipX = direction.x == -1 ? true : false;

        rb2d.MovePosition(rb2d.position + direction * moveSpeed * Time.deltaTime);
    }

    
}
