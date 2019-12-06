using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRelicInventory : MonoBehaviour
{
    private bool isRelicInventoryOpen = false;
    public GameObject equiptedRelicsUI;
    public GameObject relicsUI;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isRelicInventoryOpen)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }

    void Close()
    {
        equiptedRelicsUI.SetActive(false);
        relicsUI.SetActive(false);
        isRelicInventoryOpen = false;
    }

    void Open()
    {
        equiptedRelicsUI.SetActive(true);
        relicsUI.SetActive(true);
        isRelicInventoryOpen = true;
    }
}
