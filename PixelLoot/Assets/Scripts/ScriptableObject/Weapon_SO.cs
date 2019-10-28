using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Weapon", menuName = "Create New Weapon")]
public class Weapon_SO : Item_SO
{
    public int weaponMinDamage;
    public int weaponMaxDamage;
    public weaponType weaponType;
    public statScaling scaling;

    public GameObject projectileIfPossible;
    public GameObject particleOnHit;

    public override void Use()
    {
        if(Inventory.instance.weaponHolder.GetComponent<MeleeWeaponDamage>().weapon != null)
        {
            if (Inventory.instance.addItem(Inventory.instance.weaponHolder.GetComponent<MeleeWeaponDamage>().weapon))
            {
                Destroy(Inventory.instance.weaponHolder.gameObject.GetComponent<ThrowMeleeWeapon>());
                Destroy(Inventory.instance.weaponHolder.gameObject.GetComponent<BowScript>());
                Destroy(Inventory.instance.weaponHolder.gameObject.GetComponent<CastSpellWithStaff>());
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
            case weaponType.MELEE:
                Inventory.instance.weaponHolder.AddComponent<ThrowMeleeWeapon>();
                break;
            case weaponType.BOW:
                Inventory.instance.weaponHolder.AddComponent<BowScript>();
                break;
            case weaponType.STAFF:
                Inventory.instance.weaponHolder.AddComponent<CastSpellWithStaff>();
                break;
        }
    }
}

public enum weaponType { MELEE, STAFF, BOW }
public enum statScaling { INT, STR, DEX}
