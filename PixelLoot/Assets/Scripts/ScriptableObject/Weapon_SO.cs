using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Weapon", menuName = "Create New Weapon")]
public class Weapon_SO : Item_SO
{
    public int weaponMinDamage;
    public int weaponMaxDamage;
    public float timeBetweenAttacks;
    public weaponType weaponType;
    public statScaling scaling;

    public GameObject projectile;

    public override void Use()
    {
        if(Inventory.instance.weaponHolder.GetComponent<WeaponDamage>().weapon != null)
        {
            if (!Inventory.instance.addItem(Inventory.instance.weaponHolder.GetComponent<WeaponDamage>().weapon))            
            {
                return;
            }          
        }
        Inventory.instance.weaponHolder.GetComponent<SpriteRenderer>().sprite = itemIcon;
        Inventory.instance.weaponHolder.GetComponent<WeaponDamage>().weapon = this;
    }
}

public enum weaponType { MELEE, STAFF, BOW }
public enum statScaling { INT, STR, DEX}
