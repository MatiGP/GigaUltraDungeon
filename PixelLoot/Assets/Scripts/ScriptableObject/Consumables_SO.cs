using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Consumable", menuName = "Create New Consumable")]
public class Consumables_SO : Item_SO
{
    public enum PotionType { Health, Mana };
    public PotionType potionType;
    public int amountToRestore;

}
