using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="new character", menuName = "Create New Character")]
public class PlayableChar_SO : ScriptableObject
{
    public string characterName;
    public Sprite characterSprite;
    public Sprite characterIcon;


    public int characterBaseHealth;
    public int characterBaseMana;
    [Range(1, 10)]
    // 0 INT, 1 STR, 2 DEX, 3 VIT
    public int[] baseStats = new int[4];

    public PrimaryStat primaryStat;

    
}
public enum PrimaryStat { INT, STR, DEX };
