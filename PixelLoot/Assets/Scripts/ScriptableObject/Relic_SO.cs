using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Relic", menuName = "Create New Relic")]
public class Relic_SO : Item_SO
{
    // 0 INT, 1 STR, 2 DEX, 3 VIT
    public RelicSlot slot;
    public RelicRarity rarity;
    public AffectedStat[] affectedStats;
    public int[] values;

    public override void Use()
    {      
        for(int i = 0; i<affectedStats.Length; i++)
        {
            PlayerStats.instance.RecalculateStat((int)affectedStats[i], values[i]);
        }
    }
    
}

public enum RelicSlot { HEAD, TORSO, BOOTS, WEAPON }

public enum AffectedStat { INT, STR, DEX, VIT }

public enum RelicRarity { COMMON, MAGIC, RARE, LEGENDARY }
