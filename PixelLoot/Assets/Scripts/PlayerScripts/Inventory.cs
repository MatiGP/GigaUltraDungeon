using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public InventoryUI ui;
    public RelicInventory relicInventory;

    public GameObject weaponHolder;
    public Item_SO[] items = new Item_SO[5];

    public Relic[] relics = new Relic[9];
    
    


    private void Awake()
    {       
        instance = this;
        ui = GetComponentInChildren<InventoryUI>();
    }

    

    public bool addItem(Item_SO item)
    {
        for(int i = 0; i < 5; i++)
        {
            if(items[i] == null)
            {
                items[i] = item;
                ui.UpdateUI();
                return true;

            }
            
        }
        return false;
    }

    public bool addItem(Relic relic)
    {
        for (int i = 0; i < 9; i++)
        {
            if (relics[i] == null)
            {
                relics[i] = relic;
                relicInventory.UpdateUI(); 
                return true;

            }

        }
        return false;
    }

    public void useItem(int itemIndex)
    {
        if (items[itemIndex] != null)
        {
            items[itemIndex].Use();
        }        
        items[itemIndex] = null;
        ui.UpdateUI();
    }
}
