using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public Image[] itemIcons;
    

    private void Awake()
    {
        inventory = GetComponentInParent<Inventory>();
    }

    public void UpdateUI()
    {

        for(int n = 0; n< inventory.items.Length; n++)
        {
            if (inventory.items[n] != null)
            {
                itemIcons[n].gameObject.SetActive(true);
                itemIcons[n].sprite = inventory.items[n].itemIcon;
            }
            else
            {
                itemIcons[n].gameObject.SetActive(false);
            }
        }
    }
}
