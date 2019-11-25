using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "New Potion", menuName = "Create New Potion")]
public class Potion_SO : Item_SO
{
    public int restoreAmount;
    public PotionType whatToRestore;

    public override void Use()
    {
        Debug.Log("POTION!");
        PlayerStats.instance.RestoreHealth(restoreAmount);
    }
}

public enum PotionType { Health, Mana }
