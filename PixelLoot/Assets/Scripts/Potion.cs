using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour, IUsable
{
    public Consumables_SO consumable;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Inventory>().addItem(gameObject);

            gameObject.SetActive(false);
        }
    }  

    public void Use()
    {
        player.GetComponent<PlayerStats>().Heal(consumable.amountToRestore);
    }
}
