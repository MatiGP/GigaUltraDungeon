using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (items[0] != null || items != null)
            {
                items[0].GetComponent<IUsable>().Use();
                items[0] = null;
            }
        }
    }

    public void addItem(GameObject item)
    {
        items.Add(item);
    }
}
