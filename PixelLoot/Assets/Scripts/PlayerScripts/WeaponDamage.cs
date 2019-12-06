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
        UpdateWeaponDamage(0);
    }

    

    public void UpdateWeaponDamage(int bonus)
   {
       if((int)weapon.scaling == playerPrimaryStat)
       {
            minDamage = weapon.weaponMinDamage + stats.character.baseStats[playerPrimaryStat] / 2 + bonus;
            maxDamage = weapon.weaponMaxDamage + stats.character.baseStats[playerPrimaryStat] / 2 + bonus;
       }
       else
       {
           minDamage = weapon.weaponMinDamage + stats.character.baseStats[playerPrimaryStat] / 4 + bonus;
           maxDamage = weapon.weaponMaxDamage + stats.character.baseStats[playerPrimaryStat] / 4 + bonus;
       }
        shoot.projectile = weapon.projectile;
        

       
   }
    public int CalculateRandomWeaponDamage()
    {
        
        return Random.Range(minDamage, maxDamage); 
        
    }
    
}
