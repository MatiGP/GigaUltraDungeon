using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStatsUI : MonoBehaviour
{
    public PlayerStats stats;
    public Image characterIcon;
    public Image healthBar;
    public Image manaBar;
    public TextMeshProUGUI goldCountText;
    public TextMeshProUGUI floorText;

    private void Start()
    {
        healthBar.fillAmount = stats.GetHealthPercentage();
        manaBar.fillAmount = stats.GetManaPercentage();
        characterIcon.sprite = stats.character.characterIcon;
        
        goldCountText.text = Inventory.instance.GetGold().ToString();
    }

    public void UpdateBars()
    {
        healthBar.fillAmount = stats.GetHealthPercentage();
        manaBar.fillAmount = stats.GetManaPercentage();
    }

    public void SetFloorText(int floorNum)
    {
        floorText.text = floorNum.ToString();
    }
    public void SetFloorText(string text)
    {
        floorText.text = text;
    }
    public int GetFloorNumber()
    {
        return stats.playerSAI.currentLevel;
    }

    public void UpdateGoldText(int goldAmmount)
    {
        goldCountText.text = goldAmmount.ToString();
    }
}
