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
    Vector2 range = new Vector2(7, 7);

    // Update is called once per frame
    private void Start()
    {
        playerPosition = transform.parent;
    }
    void Update()
    {
        hasWeapon = GetComponent<MeleeWeaponDamage>().weapon ? true : false;

        if (Input.GetMouseButtonDown(0) && !canThrowWeapon && hasWeapon)
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
    }

    private void ReturnWeapon()
    {
        
        

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

        transform.position = Vector2.MoveTowards(throwStartPos, throwStartPos + range, 6 * Time.deltaTime);

        returnWeapon = Math.Abs(Vector2.Distance(throwStartPos, transform.position)) > 5f ? true : false;
    }
}
