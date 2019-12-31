using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public PlayerStats stats; //prefab setup;
    public Weapon_SO weapon; // script setup;
    

    private PrimaryStat playerPrimaryStat;
    private int minDamage;
    private int maxDamage;
    private ShootProjectile shoot;
    private void Start()
    {
        playerPrimaryStat = PlayerStats.instance.character.primaryStat;
        shoot = GetComponent<ShootProjectile>();
        UpdateWeaponDamage();
    }

    public void RawDamageBonusUpdate(int bonus)
    {
        minDamage += bonus;
        maxDamage += bonus;
    }

    private void UpdateWeaponDamage()
    {
       if((int)weapon.scaling == (int)playerPrimaryStat)
       {
            minDamage = weapon.weaponMinDamage + (stats.characterStats[(int)playerPrimaryStat] / 2);
            maxDamage = weapon.weaponMaxDamage + (stats.characterStats[(int)playerPrimaryStat] / 2);
       }   
       
        shoot.projectile = weapon.projectile;
              
    }

    public void UpdateDamageBonusFromStats(int bonus, PrimaryStat stat)
    {
        if ((int)weapon.scaling == (int)stat)
        {            
            minDamage += bonus / 2;
            maxDamage += bonus / 2;
        }
        else
        {
            minDamage += bonus / 4;
            maxDamage += bonus / 4;
        }
        
    }
    public int CalculateRandomWeaponDamage()
    {       
        return Random.Range(minDamage, maxDamage);        
    }
    
}
