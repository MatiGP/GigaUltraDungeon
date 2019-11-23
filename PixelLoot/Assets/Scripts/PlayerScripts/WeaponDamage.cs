using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public PlayerStats stats; //prefab setup;
    public Weapon_SO weapon; // script setup;
    

    private int playerPrimaryStat;
    private int minDamage;
    private int maxDamage;
    private ShootProjectile shoot;
    private void Start()
    {
        playerPrimaryStat = (int)PlayerStats.instance.character.primaryStat;
        shoot = GetComponent<ShootProjectile>();
        UpdateWeaponDamage();
    }

    

    public void UpdateWeaponDamage()
   {
       if((int)weapon.scaling == playerPrimaryStat)
       {
            minDamage = weapon.weaponMinDamage + stats.character.baseStats[playerPrimaryStat] / 2;
            maxDamage = weapon.weaponMaxDamage + stats.character.baseStats[playerPrimaryStat] / 2;
       }
       else
       {
           minDamage = weapon.weaponMinDamage + stats.character.baseStats[playerPrimaryStat] / 4;
           maxDamage = weapon.weaponMaxDamage + stats.character.baseStats[playerPrimaryStat] / 4;
       }
        shoot.projectile = weapon.projectile;
        shoot.startTimeBtwShots = weapon.timeBetweenAttacks;

       
   }
    public int CalculateRandomWeaponDamage()
    {
        return Random.Range(minDamage, maxDamage);
    }
}
