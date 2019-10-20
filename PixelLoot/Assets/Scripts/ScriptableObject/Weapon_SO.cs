using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Weapon", menuName = "Create New Weapon")]
public class Weapon_SO : Item_SO
{
    public int weaponMinDamage;
    public int weaponMaxDamage;

    public override void Use()
    {
        Inventory.instance.weaponHolder.GetComponent<SpriteRenderer>().sprite = itemIcon;
        Inventory.instance.weaponHolder.GetComponent<MeleeWeaponDamage>().weapon = this;
    }
}
