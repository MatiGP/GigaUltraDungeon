using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int minCoin;
    public int maxCoin;
    public AudioClip coinPickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SoundManager.instance.PlaySoundEffect(coinPickupSound);
            Inventory.instance.AddGold(Random.Range(minCoin, maxCoin+1));
            Destroy(gameObject);
        }
    }
}
