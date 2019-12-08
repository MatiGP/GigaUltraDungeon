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


    public int characterCurrentHealth;
    public int characterMaxHealth;
    public int characterCurrentMana;
    public PlayerStatsAndItems playerSAI;

    public int characterSTR;
    public int characterINT;
    public int characterDEX;
    public int characterVIT;

    public int characterMaxMana;
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

        controller.moveSpeed = character.moveSpeed;
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
        playerSAI.relicsInEQ = Inventory.instance.relics;
        playerSAI.wornEQ = Inventory.instance.relicInventory.equiptedRelics.wornRelics;
        playerSAI.speed = controller.moveSpeed;
        playerSAI.dexterity = characterDEX;
        playerSAI.intelect = characterINT;
        playerSAI.maxHealth = characterMaxHealth;
        playerSAI.maxMana = characterMaxMana;
        playerSAI.strength = characterSTR;
        playerSAI.vitality = characterVIT;
    }

    public void LoadState()
    {
        characterCurrentHealth = playerSAI.currentHealth;
        characterCurrentMana = playerSAI.currentMana;
        Inventory.instance.items = playerSAI.itemsInInventory;
        Inventory.instance.ui.UpdateUI();
        Inventory.instance.relics = playerSAI.relicsInEQ;
        Inventory.instance.relicInventory.equiptedRelics.wornRelics = playerSAI.wornEQ;
        Inventory.instance.relicInventory.UpdateUI();
        Inventory.instance.relicInventory.equiptedRelics.UpdateUI();
        controller.moveSpeed = playerSAI.speed;
        characterDEX = playerSAI.dexterity;
        characterINT = playerSAI.intelect;
        characterMaxHealth = playerSAI.maxHealth;
        characterMaxMana = playerSAI.maxMana;
        characterSTR = playerSAI.strength;
        characterVIT = playerSAI.vitality;
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
        characterINT += bonus;
        RecalculateMana();
        Inventory.instance.weaponHolder.GetComponent<WeaponDamage>().UpdateWeaponDamage(bonus);
        charactersUI.UpdateBars();
    }
    private void RecalculateSTR(int bonus)
    {
        characterSTR += bonus;
        Inventory.instance.weaponHolder.GetComponent<WeaponDamage>().UpdateWeaponDamage(bonus);
    }
    private void RecalculateDEX(int bonus)
    {
        characterDEX += bonus;
        Inventory.instance.weaponHolder.GetComponent<WeaponDamage>().UpdateWeaponDamage(bonus);
    }

    private void RecalculateVIT(int bonus)
    {
        characterVIT += bonus;
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
            case 4:
                Inventory.instance.weaponHolder.GetComponent<WeaponDamage>().UpdateWeaponDamage(bonus);
                break;
            case 5:
                controller.moveSpeed += bonus;
                break;
            case 6:
                Inventory.instance.weaponHolder.GetComponent<ShootProjectile>().startTimeBtwShots -= (float)bonus / 100;
                break;

        }
    }



}
