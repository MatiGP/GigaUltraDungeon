using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public PlayableChar_SO character;

    public Image healthBar;
    public Image characterImage;
    [HideInInspector]
    public int characterCurrentHealth;

    int characterMaxHealth;    
    int characterDamage;
    [HideInInspector]
    public bool TookDamage;
    
    void Awake()
    {
        characterMaxHealth = character.characterBaseHealth + character.baseStats[3];
        characterCurrentHealth = characterMaxHealth;
        characterDamage = 2 * character.baseStats[(int)character.primaryStat];
        characterImage.sprite = character.characterSprite;
    }

    void TakeDamage()
    {
        characterCurrentHealth -= 1;
        Debug.Log("Taking damage! Health remaining: " + characterCurrentHealth + " out of " + characterMaxHealth);
        UpdateBars();
        if(characterCurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void UpdateBars()
    {
        healthBar.fillAmount = (float)characterCurrentHealth / characterMaxHealth;
    }
}
