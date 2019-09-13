using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public int moveSpeed;
    bool isGrounded;

    public LayerMask groundLayerMask;


    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        spriteRenderer.flipX = direction.x == -1 ? true : false;
        animator.SetBool("IsRunning", direction.x != 0);
        rb2d.MovePosition(rb2d.position + direction * moveSpeed * Time.deltaTime);
        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f), 
            new Vector2(transform.position.x + 0.5f, transform.position.y - 0.51f), groundLayerMask);
        Debug.Log(isGrounded);
        if (GetComponent<PlayerStats>().TookDamage && isGrounded)
        {
            rb2d.AddForce(new Vector2(-20, 20), ForceMode2D.Impulse);
            GetComponent<PlayerStats>().TookDamage = false;
        }
    }

    
}
