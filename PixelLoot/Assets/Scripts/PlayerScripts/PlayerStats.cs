using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public PlayableChar_SO character;

    public Image healthBar;
    public Image characterImage;

    int characterMaxHealth;
    int characterCurrentHealth;
    int characterDamage;
    [HideInInspector]
    public bool TookDamage;
    
    void Awake()
    {
        characterMaxHealth = character.characterBaseHealth + 2*character.baseStats[3];
        characterCurrentHealth = characterMaxHealth;
        characterDamage = 2 * character.baseStats[(int)character.primaryStat];
        characterImage.sprite = character.characterSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TookDamage = true;
            TakeDamage();
            
        }
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
