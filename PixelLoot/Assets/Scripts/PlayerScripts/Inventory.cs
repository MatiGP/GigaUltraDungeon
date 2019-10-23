using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public InventoryUI ui;

    public GameObject weaponHolder;
    public Item_SO[] items;
    


    private void Awake()
    {
        instance = this;
        ui = GetComponentInChildren<InventoryUI>();
        items = new Item_SO[5];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addItem(Item_SO item)
    {
        for(int i = 0; i < 5; i++)
        {
            if(items[i] == null)
            {
                items[i] = item;
                break;
            }
            
        }
        ui.UpdateUI();
    }

    public void useItem(int itemIndex)
    {
        items[itemIndex].Use();
        items[itemIndex] = null;
        ui.UpdateUI();
    }
}
