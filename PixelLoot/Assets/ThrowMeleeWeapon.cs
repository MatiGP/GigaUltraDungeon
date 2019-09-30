using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowMeleeWeapon : MonoBehaviour
{
    public Transform playerPosition;
    bool returnWeapon;
    bool canThrowWeapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !canThrowWeapon)
        {
            canThrowWeapon = true;
        }

        if (canThrowWeapon && !returnWeapon)
        {
            ThrowWeapon();
        }
        if (returnWeapon)
        {
            ReturnWeapon();
        }
    }

    private void ReturnWeapon()
    {
        if (Vector3.Distance(playerPosition.position, transform.position) > 0.1f)
        {
            transform.position += new Vector3(-1, 0) * Time.deltaTime * 5;
            transform.Rotate(0, 0, 15);
        }
        if(Vector3.Distance(playerPosition.position, transform.position) <= 0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);   
                
        }
        returnWeapon = Vector3.Distance(playerPosition.position, transform.position) > 0.1f ? true : false;
        canThrowWeapon = Vector3.Distance(playerPosition.position, transform.position) > 0.1f ? true : false;
    }

    private void ThrowWeapon()
    {
        if(Vector3.Distance(playerPosition.position, transform.position) <= 5f)
        {
            transform.position += new Vector3(7, 0) * Time.deltaTime;
            transform.Rotate(0, 0, -15);
        }

        returnWeapon = Vector3.Distance(playerPosition.position, transform.position) > 5f ? true : false;
    }
}
