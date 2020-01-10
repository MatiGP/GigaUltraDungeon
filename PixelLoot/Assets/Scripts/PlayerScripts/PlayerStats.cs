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
    public AudioSource audioSource;

    private int characterCurrentHealth;
    private int characterMaxHealth;
    private int characterCurrentMana;
    public PlayerStatsAndItems playerSAI;

    

    public int[] characterStats = new int[4];

    private int characterMaxMana;
    private int armor;
    private SpriteRenderer spriteRenderer;
    private PlayerController controller;
    [HideInInspector]
    public static bool isPlayerAlive;

    void Awake()
    {                 
        instance = this;
        characterStats = character.baseStats;
        DeathMenuScript.isPlayerDead = false;
        characterMaxHealth = character.characterBaseHealth + characterStats[3];
        characterCurrentHealth = characterMaxHealth;
        characterMaxMana = character.characterBaseMana + characterStats[0];
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
        
        changeColorRed();
        if (characterCurrentHealth <= 0)
        {
            DeathMenuScript.isPlayerDead = true;
            controller.vcam.enabled = isPlayerAlive;
            AudioSource.PlayClipAtPoint(character.deathSound, transform.position, 1f);
            Destroy(gameObject);
        }
        else
        {
            audioSource.PlayOneShot(character.painSound);
        }
        

        charactersUI.UpdateBars();
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
        playerSAI.dexterity = characterStats[2];
        playerSAI.intelect = characterStats[0];
        playerSAI.maxHealth = characterMaxHealth;
        playerSAI.maxMana = characterMaxMana;
        playerSAI.strength = characterStats[1];
        playerSAI.vitality = characterStats[3];
        playerSAI.gold = Inventory.instance.GetGold();
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
        characterStats[2] = playerSAI.dexterity;
        characterStats[0] = playerSAI.intelect;
        characterMaxHealth = playerSAI.maxHealth;
        characterMaxMana = playerSAI.maxMana;
        characterStats[1] = playerSAI.strength;
        characterStats[3] = playerSAI.vitality;
        Inventory.instance.AddGold(playerSAI.gold);
    }

    private void RecalculateHealth()
    {
        characterMaxHealth = character.characterBaseHealth + characterStats[3];
    }

    private void RecalculateMana()
    {
        characterMaxMana = character.characterBaseMana + characterStats[0];

    }

    private void RecalculateINT(int bonus)
    {
        characterStats[0] += bonus;
        RecalculateMana();
        Inventory.instance.weaponHolder.GetComponent<WeaponDamage>().UpdateDamageBonusFromStats(bonus, PrimaryStat.INT);
        charactersUI.UpdateBars();
    }
    private void RecalculateSTR(int bonus)
    {
        characterStats[1] += bonus;
        Inventory.instance.weaponHolder.GetComponent<WeaponDamage>().UpdateDamageBonusFromStats(bonus, PrimaryStat.STR);
    }
    private void RecalculateDEX(int bonus)
    {
        characterStats[2] += bonus;
        Inventory.instance.weaponHolder.GetComponent<WeaponDamage>().UpdateDamageBonusFromStats(bonus, PrimaryStat.DEX);
    }

    private void RecalculateVIT(int bonus)
    {
        characterStats[3] += bonus;
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
                Inventory.instance.weaponHolder.GetComponent<WeaponDamage>().RawDamageBonusUpdate(bonus);
                break;
            case 5:
                controller.moveSpeed += bonus;
                break;
            case 6:
                Inventory.instance.weaponHolder.GetComponent<ShootProjectile>().startTimeBtwShots -= (float)bonus / 100;
                break;

        }
    }

    public int GetCurrentMana()
    {
        return characterCurrentMana;
    }

}
