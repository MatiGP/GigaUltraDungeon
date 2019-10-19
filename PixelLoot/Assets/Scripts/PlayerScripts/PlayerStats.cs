using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public PlayableChar_SO character;
    public static PlayerStats instance;

    public Image healthBar;
    public Image characterImage;
    public Image manaBar;
    [HideInInspector]
    public int characterCurrentHealth;
    [HideInInspector]
    public int characterCurrentMana;

    int characterMaxHealth;
    int characterMaxMana;
    [HideInInspector]
    public bool TookDamage;
    [HideInInspector]
    public bool canCastSpells;
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        characterMaxHealth = character.characterBaseHealth + character.baseStats[3];
        characterCurrentHealth = characterMaxHealth;
        characterMaxMana = character.characterBaseMana + character.baseStats[0];
        characterCurrentMana = characterMaxMana;

        characterImage.sprite = character.characterSprite;
    }

    public void TakeDamage(int damageTaken)
    {
        characterCurrentHealth -= damageTaken;
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
        manaBar.fillAmount = (float)characterCurrentMana / characterMaxMana;
    }

    public void RestoreHealth(int healthAmount)
    {
        characterCurrentHealth += healthAmount;
        if(characterCurrentHealth > characterMaxHealth)
        {
            characterCurrentHealth = characterCurrentHealth - (characterCurrentHealth % characterMaxHealth);
        }
        UpdateBars();
    }

    public void RestoreMana(int manaAmount)
    {
        characterCurrentMana += manaAmount;
        if (characterCurrentMana > characterMaxMana)
        {
            characterCurrentMana = characterCurrentMana - (characterCurrentMana % characterMaxMana);
        }
        UpdateBars();
    }
}
