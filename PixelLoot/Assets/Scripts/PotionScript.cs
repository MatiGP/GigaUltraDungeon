using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour
{
    public Consumables_SO potion;

    public void UsePotion()
    {
        if(potion.potionType == Consumables_SO.PotionType.Health)
        {
            GetComponent<PlayerStats>().characterCurrentHealth += potion.amountToRestore;
        }      
    }
}
