using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashScirpt : MonoBehaviour
{
    public int dashForce;
    public float dashCooldown;
    public GameObject ghost;
    public Image skillno1;
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
            skillno1.fillAmount = 1;
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
            skillno1.fillAmount = currentDashCooldown / dashCooldown;
        }
        
    }

    void Dash()
    {
        rb.velocity = dashDirection * dashForce;
        Instantiate(ghost, transform.position, transform.rotation);
    }
}
