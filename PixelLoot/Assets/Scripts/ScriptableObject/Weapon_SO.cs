using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Weapon", menuName = "Create New Weapon")]
public class Weapon_SO : Item_SO
{
    public int weaponMinDamage;
    public int weaponMaxDamage;
    public weaponType weaponType;

    public GameObject projectileIfPossible;
    public GameObject player;

    public override void Use()
    {
        if(Inventory.instance.weaponHolder.GetComponent<MeleeWeaponDamage>().weapon != null)
        {
            if (Inventory.instance.addItem(this))
            {
           
            }
            else
            {
                return;
            }          

        }
        Inventory.instance.weaponHolder.GetComponent<SpriteRenderer>().sprite = itemIcon;
        Inventory.instance.weaponHolder.GetComponent<MeleeWeaponDamage>().weapon = this;

        switch (weaponType)
        {
            case weaponType.Melee:
                Inventory.instance.weaponHolder.AddComponent<ThrowMeleeWeapon>();
                break;
            case weaponType.Bow:
                Inventory.instance.weaponHolder.AddComponent<BowScript>();
                break;
            case weaponType.Staff:
                Inventory.instance.weaponHolder.AddComponent<CastSpellWithStaff>();
                break;
        }
    }
}

public enum weaponType { Melee, Staff, Bow }
