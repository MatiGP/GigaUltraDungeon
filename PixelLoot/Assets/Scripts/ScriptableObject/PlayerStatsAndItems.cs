using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "playerState", menuName = "playerState")]
public class PlayerStatsAndItems : ScriptableObject
{
    public int currentHealth;
    public int currentMana;
    public Item_SO[] itemsInInventory = new Item_SO[5];

}
