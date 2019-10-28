using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public PlayableChar_SO character;
    public CharacterStatsUI charactersUI;
    public static PlayerStats instance;

    public int characterCurrentHealth;
    public int characterCurrentMana;
    private int characterMaxHealth;
    private int characterMaxMana;
  
    [HideInInspector]
    public bool canCastSpells;
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        characterMaxHealth = character.characterBaseHealth + character.baseStats[3];
        characterCurrentHealth = characterMaxHealth;
        characterMaxMana = character.characterBaseMana + character.baseStats[0];
        characterCurrentMana = characterMaxMana;
    }    

    public void TakeDamage(int damageTaken)
    {
        characterCurrentHealth -= damageTaken;
        Debug.Log("Taking damage! Health remaining: " + characterCurrentHealth + " out of " + characterMaxHealth);
        charactersUI.UpdateBars();
        if (characterCurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }


    public void RestoreHealth(int healthAmount)
    {
        characterCurrentHealth += healthAmount;
        charactersUI.UpdateBars();
        if (characterCurrentHealth > characterMaxHealth)
        {
            characterCurrentHealth = characterCurrentHealth - (characterCurrentHealth % characterMaxHealth);
        }
    }

    public void RestoreMana(int manaAmount)
    {
        characterCurrentMana += manaAmount;
        charactersUI.UpdateBars();
        if (characterCurrentMana > characterMaxMana)
        {
            characterCurrentMana = characterCurrentMana - (characterCurrentMana % characterMaxMana);
        }
        canCastSpells = true;
    }

    public void SpendMana(int manaAmount)
    {
        characterCurrentMana -= manaAmount;
        charactersUI.UpdateBars();
    }

    public float GetHealthPercentage()
    {
        return (float)characterCurrentHealth / characterMaxHealth;
    }
    public float GetManaPercentage()
    {
        return (float)characterCurrentMana / characterMaxMana;
    }
}
