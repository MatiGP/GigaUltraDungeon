using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public PlayableChar_SO character;
    public CharacterStatsUI charactersUI;
    public static PlayerStats instance;
    public GameObject deathPanel;


    private int characterCurrentHealth;
    private int characterMaxHealth;
    public int characterCurrentMana;
    public PlayerStatsAndItems playerSAI;

    public int characterSTR;
    public int characterINT;
    public int characterDEX;
    public int characterVIT;

    private int characterMaxMana;
    private int armor;
    private SpriteRenderer spriteRenderer;
    private PlayerController controller;
    [HideInInspector]
    public static bool isPlayerAlive;

    void Awake()
    {                 
        instance = this; 

        characterINT = character.baseStats[0];
        characterSTR = character.baseStats[1];
        characterDEX = character.baseStats[2];
        characterVIT = character.baseStats[3];

        DeathMenuScript.isPlayerDead = false;
        characterMaxHealth = character.characterBaseHealth + characterVIT;
        characterCurrentHealth = characterMaxHealth;
        characterMaxMana = character.characterBaseMana + characterINT;
        characterCurrentMana = characterMaxMana;

        controller = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }     
    public void RestoreMana(int manaAmount)
    {
        characterCurrentMana += manaAmount;

        if (characterCurrentMana > characterMaxMana)
        {
            characterCurrentMana = characterCurrentMana - (characterCurrentMana % characterMaxMana);
        }
        charactersUI.UpdateBars();
    }
    public void SpendMana(int manaAmount)
    {
        characterCurrentMana -= manaAmount;
        charactersUI.UpdateBars();
    }
    
    public float GetManaPercentage()
    {
        return (float)characterCurrentMana / characterMaxMana;
    }
    public void TakeDamage(int damageTaken)
    {
        characterCurrentHealth -= (damageTaken - armor);
        charactersUI.UpdateBars();

        changeColorRed();
        if (characterCurrentHealth <= 0)
        {
            DeathMenuScript.isPlayerDead = true;
            controller.vcam.enabled = isPlayerAlive;

            Destroy(gameObject);
        }
        StartCoroutine(changeColorWhite());
    }

    public void RestoreHealth(int healthAmount)
    {
        characterCurrentHealth += healthAmount;
        if (characterCurrentHealth > characterMaxHealth)
        {
            characterCurrentHealth = characterCurrentHealth - (characterCurrentHealth % characterMaxHealth);
        }
        charactersUI.UpdateBars();
    }
    public float GetHealthPercentage()
    {
        return (float)characterCurrentHealth / characterMaxHealth;
    }

    private void Update()
    {
        
        if (isPlayerAlive)
        {
            AttachTheCamera();
            isPlayerAlive = false;
        }

    }

    public void AttachTheCamera()
    {
        controller.vcam.enabled = true;
    }
    
    void changeColorRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
    IEnumerator changeColorWhite()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        yield return null;
    }

    public void SetArmor(int armor)
    {
        this.armor = armor;
    }

    public void SaveState()
    {
        playerSAI.currentHealth = characterCurrentHealth;
        playerSAI.currentMana = characterCurrentMana;
        playerSAI.itemsInInventory = Inventory.instance.items;
    }

    public void LoadState()
    {
        characterCurrentHealth = playerSAI.currentHealth;
        characterCurrentMana = playerSAI.currentMana;
        Inventory.instance.items = playerSAI.itemsInInventory;
        Inventory.instance.ui.UpdateUI();
    }

    private void RecalculateHealth()
    {
        characterMaxHealth = character.characterBaseHealth + characterVIT;
    }

    private void RecalculateMana()
    {
        characterMaxMana = character.characterBaseMana + characterINT;

    }

    private void RecalculateINT(int bonus)
    {
        characterINT = character.baseStats[0] + bonus;
        RecalculateMana();
        Inventory.instance.UpdateDamage();
        charactersUI.UpdateBars();
    }
    private void RecalculateSTR(int bonus)
    {
        characterSTR = character.baseStats[1] + bonus;
        Inventory.instance.UpdateDamage();
    }
    private void RecalculateDEX(int bonus)
    {
        characterDEX = character.baseStats[2] + bonus;
        Inventory.instance.UpdateDamage();
    }

    private void RecalculateVIT(int bonus)
    {
        characterVIT = character.baseStats[3] + bonus;
        RecalculateHealth();
        charactersUI.UpdateBars();
        
    }

    public void RecalculateStat(int index, int bonus)
    {
        switch (index)
        {
            case 0:
                RecalculateINT(bonus);
                break;
            case 1:
                RecalculateSTR(bonus);
                break;
            case 2:
                RecalculateDEX(bonus);
                break;
            case 3:
                RecalculateVIT(bonus);
                break;
        }
    }



}
