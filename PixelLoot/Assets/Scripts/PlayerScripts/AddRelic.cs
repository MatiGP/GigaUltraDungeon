using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRelic : MonoBehaviour
{
    public Relic_SO relic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {            
            if (Inventory.instance.addItem(new Relic(relic.itemIcon, relic.itemName, relic.slot, relic.rarity, relic.affectedStats, relic.values)))
            {
                 
                Destroy(gameObject);
            }
        }
    }
}
