using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RelicInventory : MonoBehaviour
{
    public Inventory inventory;
    public EquiptedRelics equiptedRelics;

    public Button[] relicButttons;
    [Space(4)]
    public GameObject relicInfoPanel;
    public TextMeshProUGUI relicName;
    public TextMeshProUGUI[] relicStats;
    public TextMeshProUGUI[] relicStatsValues;
    public TextMeshProUGUI slotValue;
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

    public void SetupRelicInfoWindow(int index)
    {
        relicInfoPanel.SetActive(true);

        relicName.text = inventory.relics[index].itemName;

        for(int i = 0; i < inventory.relics[index].affectedStats.Length; i++)
        {
            relicStats[i].text = inventory.relics[index].affectedStats[i].ToString();
            relicStatsValues[i].text = inventory.relics[index].values[i].ToString();
        }
        slotValue.text = inventory.relics[index].slot.ToString();
    }

    public void DeactivateRelicInfoWindow()
    {
        relicInfoPanel.SetActive(false);
        for(int i = 0; i < 4; i++)
        {
            relicStats[i].text = "";
            relicStatsValues[i].text = "";

        }
        slotValue.text = "";
    }

}
