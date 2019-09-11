using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayableChar_SO character;

    int characterHealth;
    int characterDamage;
    
    void Awake()
    {
        characterHealth = character.characterBaseHealth + 2*character.baseStats[3];
        characterDamage = 2 * character.baseStats[(int)character.primaryStat];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
