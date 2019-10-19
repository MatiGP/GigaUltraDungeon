using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int moveSpeed;
    public int jumpForce = 100;

    private Rigidbody2D rb2d;
    private Animator animator;
    private bool isGrounded;
    private bool facingRight = true;
    private Vector2 direction;

    public LayerMask groundLayerMask;


    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        rb2d.velocity = direction * moveSpeed;
        animator.SetBool("IsRunning", direction.x != 0);

        if (Input.GetKeyDown(KeyCode.X))
        {
            GetComponent<PlayerStats>().TakeDamage(4);
        }

        if (GetComponent<PlayerStats>() && isGrounded)
        {
            KnockbackOnDamage();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Inventory.instance.useItem(0);
        }
    }

    private void LateUpdate()
    {
        Vector3 localScale = transform.localScale;

        if(direction.x > 0)
        {
            facingRight = true;
        }else if(direction.x < 0)
        {
            facingRight = false;
        }

        if((facingRight && (localScale.x < 0)) || (!facingRight && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }

        transform.localScale = localScale;

        isGrounded = Physics2D.OverlapArea(
            new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f),
            new Vector2(transform.position.x + 0.5f, transform.position.y - 0.51f), groundLayerMask
            );
    }

    void KnockbackOnDamage()
    {
        rb2d.AddForce(new Vector2(-20, 20));
        GetComponent<PlayerStats>().TookDamage = false;
    }


}
