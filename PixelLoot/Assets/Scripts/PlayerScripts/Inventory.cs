using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public GameObject weaponHolder;
    public List<Item_SO> items = new List<Item_SO>();
    


    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addItem(Item_SO item)
    {
        items.Add(item);
    }

    public void useItem(int itemIndex)
    {
        items[itemIndex].Use();
        items.RemoveAt(itemIndex);
    }
}
