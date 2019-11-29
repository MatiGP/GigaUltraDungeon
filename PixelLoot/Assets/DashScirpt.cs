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
    private Vector2 dashDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDashCooldown <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {                
                dashDirection = pc.GetPlayerDirection();
                Dash();
                currentDashCooldown = dashCooldown;
            }            
        }
        else
        {
            currentDashCooldown -= Time.deltaTime;
        }
        
    }

    void Dash()
    {
        rb.velocity = dashDirection * dashForce;
    }
}
