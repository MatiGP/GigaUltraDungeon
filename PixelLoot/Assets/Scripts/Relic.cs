using UnityEngine;

public class Relic
{
    public Sprite itemIcon;
    public string itemName;
    public RelicSlot slot;
    public RelicRarity rarity;
    public AffectedStat[] affectedStats;
    public int[] values;

    public Relic(Sprite icon, string name)
    {
        
    }

    public Relic(Sprite icon, string name, RelicSlot slot, RelicRarity rarity, AffectedStat[] affectedStats, int[] values)
    {
        itemIcon = icon;
        itemName = name;
        this.slot = slot;
        this.rarity = rarity;
        this.affectedStats = affectedStats;
        this.values = values;
    }

    public void Use()
    {
        for (int i = 0; i < affectedStats.Length; i++)
        {
            PlayerStats.instance.RecalculateStat((int)affectedStats[i], values[i]);
        }
    }

    public void Reroll()
    {
        int newSlot = Random.Range(0, 4);
        int newRarity = Random.Range(0, 4);
        affectedStats = new AffectedStat[newRarity + 1];
        values = new int[newRarity + 1];
        rarity = (RelicRarity)newRarity;
        slot = (RelicSlot)newSlot;

        for (int i = 0; i < newRarity; i++)
        {
            affectedStats[i] = (AffectedStat)Random.Range(0, 7);
            values[i] = Random.Range(-3, 11);
        }


    }
}
