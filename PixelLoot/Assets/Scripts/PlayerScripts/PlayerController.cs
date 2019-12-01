using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{  
    public int moveSpeed;
    [HideInInspector]
    public CinemachineVirtualCamera vcam;
    private Rigidbody2D rb2d;
    private Animator animator;
    private bool facingRight = true;
    private Vector2 direction;

    private void Awake()
    {
        //DontDestroyOnLoad(this);
        GameObject vCamGameObject = GameObject.FindGameObjectWithTag("VirtualCamera");
        vcam = vCamGameObject.GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        vcam.Follow = transform;
        
    }

    private void Update()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction = direction.normalized;

        rb2d.velocity = direction * moveSpeed;
        animator.SetBool("IsRunning", direction.x != 0 || direction.y != 0);

        #region Inventory Buttons
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Inventory.instance.useItem(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Inventory.instance.useItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Inventory.instance.useItem(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Inventory.instance.useItem(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Inventory.instance.useItem(4);
        }
        #endregion

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

    public Vector2 GetPlayerDirection()
    {
        return direction;
    }


}
