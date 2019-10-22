using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowMeleeWeapon : MonoBehaviour
{
    private Transform playerPosition;
    private Vector2 throwStartPos;
    bool returnWeapon;
    bool canThrowWeapon;
    bool hasWeapon;
    Vector3 scale;

    // Update is called once per frame
    private void Start()
    {
        playerPosition = transform.parent;
    }
    void Update()
    {
        hasWeapon = GetComponent<MeleeWeaponDamage>().weapon ? true : false;

        if (Input.GetKeyDown(KeyCode.R) && !canThrowWeapon && hasWeapon)
        {
            canThrowWeapon = true;
            throwStartPos = new Vector2(playerPosition.position.x, playerPosition.position.y);
            scale = playerPosition.localScale;
        }

        if (canThrowWeapon && !returnWeapon)
        {
            ThrowWeapon();
        }
        if (returnWeapon)
        {
            ReturnWeapon();
        }
        Debug.Log(playerPosition.position);
    }

    private void ReturnWeapon()
    {
        
        if (Math.Abs(Vector2.Distance(playerPosition.position, transform.position)) > 0.1f)
        {
            if(scale.x < 0)
            {
                transform.position -= (new Vector3(-5, 0) * Time.deltaTime);
            }
            else

            {
                transform.position += (new Vector3(-5, 0) * Time.deltaTime);
            }      
            transform.Rotate(0, 0, 15);
        }
        if(Math.Abs(Vector2.Distance(playerPosition.position, transform.position)) <= 0.1f)
        {
            gameObject.transform.parent = playerPosition.gameObject.transform;
            transform.localScale = new Vector3(1, 1);
            transform.rotation = Quaternion.Euler(0, 0, 0);
                
        }
        returnWeapon = Math.Abs(Vector2.Distance(playerPosition.localPosition, transform.position)) > 0.1f ? true : false;
        canThrowWeapon = Math.Abs(Vector2.Distance(playerPosition.localPosition, transform.position)) > 0.1f ? true : false;
    }

    private void ThrowWeapon()
    {
        gameObject.transform.parent = null;

        if (Math.Abs(Vector2.Distance(throwStartPos, transform.position)) <= 5f)
        {
            if(scale.x < 0)
            {
                transform.position -= new Vector3(7, 0) * Time.deltaTime;
            }
            else
            {
                transform.position += new Vector3(7, 0) * Time.deltaTime;
            }
            
            transform.Rotate(0, 0, -15);
        }

        returnWeapon = Math.Abs(Vector2.Distance(throwStartPos, transform.position)) > 5f ? true : false;
    }
}
