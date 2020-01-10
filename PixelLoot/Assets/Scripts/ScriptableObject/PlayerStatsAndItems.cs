using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "playerState", menuName = "playerState")]
public class PlayerStatsAndItems : ScriptableObject
{
    public int currentHealth;
    public int maxHealth;
    public int currentMana;
    public int maxMana;
    public int dexterity;
    public int intelect;
    public int strength;
    public int vitality;
    public Item_SO[] itemsInInventory = new Item_SO[5];
    public int currentLevel;
    public Relic[] relicsInEQ = new Relic[9];
    public Relic[] wornEQ = new Relic[4];
    public int speed;
    public int gold;

}
