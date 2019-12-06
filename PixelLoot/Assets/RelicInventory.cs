using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RelicInventory : MonoBehaviour
{
    public Inventory inventory;
    public EquiptedRelics equiptedRelics;

    public Button[] relicButttons;


    public void UpdateUI()
    {
        for(int i = 0; i < 9; i++)
        {
            if(inventory.relics[i] != null)
            {
                relicButttons[i].gameObject.SetActive(true);
                relicButttons[i].image.sprite = inventory.relics[i].itemIcon;
            }
            else
            {
                relicButttons[i].gameObject.SetActive(false);
            }
        }
    }

    public void UseRelic(int index)
    {
        if(equiptedRelics.wornRelics[(int)inventory.relics[index].slot] != null)
        {
            equiptedRelics.Unequip((int)inventory.relics[index].slot);
            Relic_SO tmpRelic = equiptedRelics.wornRelics[(int)inventory.relics[index].slot];
            EquipRelic(index, tmpRelic);
        }
        else
        {
            EquipRelic(index, null);
        }


    }

    private void EquipRelic(int index, Relic_SO relic)
    {
        inventory.relics[index].Use();
        equiptedRelics.wornRelics[(int)inventory.relics[index].slot] = inventory.relics[index];
        equiptedRelics.UpdateUI();
        inventory.relics[index] = relic;
        UpdateUI();
    }

}
