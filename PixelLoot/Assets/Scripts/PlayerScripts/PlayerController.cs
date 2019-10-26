using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int moveSpeed;

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
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction = direction.normalized;

        rb2d.velocity = direction * moveSpeed;
        animator.SetBool("IsRunning", direction.x != 0 || direction.y != 0);

        if (Input.GetKeyDown(KeyCode.X))
        {
            Inventory.instance.items[0].Use();
        }
      
    }

    private void LateUpdate()
    {     

        if(direction.x > 0)
        {
            facingRight = true;
        }else if(direction.x < 0)
        {
            facingRight = false;
        }

        if ((facingRight && (transform.rotation.y <= 180)))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if(!facingRight && (transform.rotation.y >= 0))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
      
    }


}
