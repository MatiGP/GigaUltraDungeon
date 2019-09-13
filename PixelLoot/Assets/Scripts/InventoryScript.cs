using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public CanvasGroup inventory;
    private bool isInventoryOpen;

    private List<Item_SO> itemsInInventory;

    private void Awake()
    {
        itemsInInventory = new List<Item_SO>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isInventoryOpen)
            {
                ShowInventory();
            }
            else
            {
                HideInventory();
            }

        }
    }

    void ShowInventory()
    {
        isInventoryOpen = true;
        inventory.alpha = 1;
    }

    void HideInventory()
    {
        isInventoryOpen = false;
        inventory.alpha = 0;
    }

    public void AddItemToInventory(Item_SO item)
    {
        itemsInInventory.Add(item);
    }
}
