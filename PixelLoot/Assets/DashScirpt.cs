using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScirpt : MonoBehaviour
{
    public int dashForce;
    public float dashCooldown;
    private float currentDashCooldown;
    private Rigidbody2D rb;
    private PlayerController pc;
    private CapsuleCollider2D capsuleCollider;
    private Vector2 dashDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDashCooldown <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                capsuleCollider.enabled = false;
                Debug.Log(capsuleCollider.enabled);
                dashDirection = pc.GetPlayerDirection();
                Dash();
                currentDashCooldown = dashCooldown;
                capsuleCollider.enabled = true;
            }            
        }
        else
        {
            currentDashCooldown -= Time.deltaTime;
        }
        
    }

    void Dash()
    {
        Debug.Log("Hop!" + dashDirection);
        rb.velocity = dashDirection * dashForce;
    }
}
