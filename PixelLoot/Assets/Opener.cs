using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opener : MonoBehaviour
{
    public bool isActive = true;
    public GameObject objectToEnable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            objectToEnable.SetActive(isActive);
        }
    }
}
