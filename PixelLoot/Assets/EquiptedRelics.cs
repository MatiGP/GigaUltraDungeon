using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquiptedRelics : MonoBehaviour
{
    public Relic_SO[] wornRelics = new Relic_SO[4];
    public Button[] wornRelicsButtons;
    public RelicInventory relicInventory;
    
    public void UpdateUI()
    {
       for(int i = 0; i < 4; i++)
        {
            if(wornRelics[i] != null)
            {
                wornRelicsButtons[i].gameObject.SetActive(true);
                wornRelicsButtons[i].image.sprite = wornRelics[i].itemIcon;
            }
            else
            {
                wornRelicsButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void Unequip(int index)
    {
        if (Inventory.instance.addItem(wornRelics[index]))
        {
            for (int i = 0; i < wornRelics[index].affectedStats.Length; i++)
            {
                PlayerStats.instance.RecalculateStat((int)wornRelics[index].affectedStats[i], -wornRelics[index].values[i]);
            }          
            wornRelics[index] = null;
            UpdateUI();
            relicInventory.DeactivateRelicInfoWindow();
            relicInventory.UpdateUI();           
        }        
    }

  
}
