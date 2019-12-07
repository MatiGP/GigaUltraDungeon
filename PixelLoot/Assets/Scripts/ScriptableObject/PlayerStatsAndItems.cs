using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "playerState", menuName = "playerState")]
public class PlayerStatsAndItems : ScriptableObject
{
    public int currentHealth;
    public int currentMana;
    public Item_SO[] itemsInInventory = new Item_SO[5];
    public int currentLevel;
    public Relic_SO[] relicsInEQ = new Relic_SO[9];
    public Relic_SO[] wornEQ = new Relic_SO[4];

}
