using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public PlayerStats stats; //prefab setup;
    public Weapon_SO weapon; // script setup;

    private int playerPrimaryStat = (int)PlayerStats.instance.character.primaryStat;
    private int minDamage;
    private int maxDamage;
    private int randomDamage;

   public void UpdateWeaponDamage()
   {
       if((int)weapon.scaling == playerPrimaryStat)
       {
            minDamage = weapon.weaponMinDamage + stats.character.baseStats[playerPrimaryStat];
            maxDamage = weapon.weaponMaxDamage + stats.character.baseStats[playerPrimaryStat];
       }
       else
       {
           minDamage = weapon.weaponMinDamage + stats.character.baseStats[playerPrimaryStat] / 3;
           maxDamage = weapon.weaponMaxDamage + stats.character.baseStats[playerPrimaryStat] / 3;
       }
   }
}
