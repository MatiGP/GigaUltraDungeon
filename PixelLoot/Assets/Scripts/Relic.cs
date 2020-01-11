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
        itemIcon = icon;
        itemName = name;
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
            if(affectedStats[i] == AffectedStat.ATKSPD)
            {
                values[i] = Random.Range(-30, 40);
            }
            else if(affectedStats[i] == AffectedStat.MVMSPD)
            {
                values[i] = Random.Range(-2, 2);
            }
            else
            {
                values[i] = Random.Range(-3, 11);
            }
        }

        itemName = GenerateName();


    }

    string GenerateName()
    {
        
        return GeneratePrefix() + " Relikwia " + GenerateAffix();
    }

    int FindIndexOfTheGreatestValueForAffixes()
    {
        int tmpval = -9;
        int index = -1;

        for(int i = 0; i<values.Length; i++)
        {
            if((int)affectedStats[i] < 4)
            {
                if(tmpval < values[i])
                {
                    tmpval = values[i];
                    index = i;
                }
                
            }
        }

        return index;
    }

    int FindIndexOfTheGreatestValueForPrefixes()
    {
        int tmpval = -90;
        int index = -1;

        for (int i = 0; i < values.Length; i++)
        {
            if ((int)affectedStats[i] > 3)
            {
                if (tmpval < values[i])
                {
                    tmpval = values[i];
                    index = i;
                }

            }
        }

        return index;
    }

    string GenerateAffix()
    {
        string affix = "";

        int index = FindIndexOfTheGreatestValueForAffixes();

        if(index == -1)
        {
            return affix;
        }

        switch (affectedStats[index])
        {
            case AffectedStat.INT:
                if(values[index] > 3)
                {
                    affix = "Inteligencji";
                }
                else
                {
                    affix = "Glupoty";
                }
                break;
            case AffectedStat.STR:
                if (values[index] > 3)
                {
                    affix = "Wojownika";
                }
                else
                {
                    affix = "Slabeusza";
                }
                break;
            case AffectedStat.VIT:
                if (values[index] > 3)
                {
                    affix = "Niemierlenosci";
                }
                else
                {
                    affix = "Zarazy";
                }
                break;
            case AffectedStat.DEX:
                if (values[index] > 3)
                {
                    affix = "Zrecznosci";
                }
                else
                {
                    affix = "Niezdarnosci";
                }
                break;
        }
        return affix;
    }

    string GeneratePrefix()
    {
        string prefix = "";

        int index = FindIndexOfTheGreatestValueForPrefixes();

        if (index == -1)
        {
            return prefix;
        }

        switch (affectedStats[index])
        {
            case AffectedStat.DMG:
                if (values[index] > 4)
                {
                    prefix = "Brutalna";
                }
                else
                {
                    prefix = "Pokojowa";
                }
                break;
            case AffectedStat.ATKSPD:
                if (values[index] > 15)
                {
                    prefix = "Nadzwiekowa";
                }
                else
                {
                    prefix = "Ociezala";
                }
                break;
            case AffectedStat.MVMSPD:
                if (values[index] > 0)
                {
                    prefix = "Ruchliwa";
                }
                else
                {
                    prefix = "Leniwa";
                }
                break;
           
        }

        return prefix;
    }
    




}
