using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatsUI : MonoBehaviour
{
    public PlayerStats stats;
    public Image characterIcon;
    public Image healthBar;
    public Image manaBar;

    private void Start()
    {
        healthBar.fillAmount = stats.GetHealthPercentage();
        manaBar.fillAmount = stats.GetManaPercentage();
        characterIcon.sprite = stats.character.characterSprite;
    }

    public void UpdateBars()
    {
        healthBar.fillAmount = stats.GetHealthPercentage();
        manaBar.fillAmount = stats.GetManaPercentage();
    }

}
