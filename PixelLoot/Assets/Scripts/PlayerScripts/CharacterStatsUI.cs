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
    public TextMeshProUGUI floorText;

    private void Start()
    {
        healthBar.fillAmount = stats.GetHealthPercentage();
        manaBar.fillAmount = stats.GetManaPercentage();
        characterIcon.sprite = stats.character.characterIcon;
        SetFloorText(stats.playerSAI.currentLevel);
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
    public int GetFloorNumber()
    {
        return PlayerStats.instance.playerSAI.currentLevel;
    }

}
