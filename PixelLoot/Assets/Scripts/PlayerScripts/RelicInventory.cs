using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RelicInventory : MonoBehaviour
{
    public Inventory inventory;
    public EquiptedRelics equiptedRelics;
    public Sprite disposeRelicSprite;
    public Sprite sellRelicSprite;
    public Button[] relicButttons;
    public Button[] sellOrDisposeButtons;
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
        if (RelicForge.isForgeUiOpen)
        {
            RelicForge.instance.PutRelicInTheForge(inventory.relics[index]);
            DisposeRelic(index);
        }
        else
        {
            if (equiptedRelics.wornRelics[(int)inventory.relics[index].slot] != null)
            {
                equiptedRelics.Unequip((int)inventory.relics[index].slot);
                Relic tmpRelic = equiptedRelics.wornRelics[(int)inventory.relics[index].slot];
                EquipRelic(index, tmpRelic);
            }
            else
            {
                EquipRelic(index, null);
            }
        }

    }

    private void EquipRelic(int index, Relic relic)
    {
        inventory.relics[index].Use();
        equiptedRelics.wornRelics[(int)inventory.relics[index].slot] = inventory.relics[index];
        equiptedRelics.UpdateUI();
        inventory.relics[index] = relic;
        DeactivateRelicInfoWindow();
        UpdateUI();
    }

    public void SetupRelicInfoWindow(int index)
    {
        relicInfoPanel.SetActive(true);

        relicName.text = inventory.relics[index].itemName;
        SwapColor(inventory.relics[index]);

        for (int i = 0; i < inventory.relics[index].affectedStats.Length; i++)
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

    void SwapColor(Relic relic)
    {
        switch (relic.rarity)
        {
            case RelicRarity.COMMON:
                relicName.color = Color.white;
                break;
            case RelicRarity.LEGENDARY:
                relicName.color = new Color32(255, 102, 0, 255);
                break;
            case RelicRarity.MAGIC:
                relicName.color = new Color32(92, 214, 92, 255);
                break;
            case RelicRarity.RARE:
                relicName.color = Color.yellow;
                break;
        }
    }

    public void SetupEquipedRelicInfoWindow(int index)
    {
        relicInfoPanel.SetActive(true);

        relicName.text = equiptedRelics.wornRelics[index].itemName;
        SwapColor(equiptedRelics.wornRelics[index]);

        for (int i = 0; i < equiptedRelics.wornRelics[index].affectedStats.Length; i++)
        {
            relicStats[i].text = equiptedRelics.wornRelics[index].affectedStats[i].ToString();
            relicStatsValues[i].text = equiptedRelics.wornRelics[index].values[i].ToString();
        }
        slotValue.text = equiptedRelics.wornRelics[index].slot.ToString();
    }


    public void DisposeRelic(int index)
    {
        inventory.relics[index] = null;
        DeactivateRelicInfoWindow();
        UpdateUI();
    }

    public void SwapSpriteSell()
    {
        foreach(Button b in sellOrDisposeButtons)
        {
            b.image.sprite = sellRelicSprite;
        }
    }

    public void SwapSpriteDispose()
    {
        foreach (Button b in sellOrDisposeButtons)
        {
            b.image.sprite = disposeRelicSprite;
        }
    }

}
