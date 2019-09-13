using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="new character", menuName = "Create New Character")]
public class PlayableChar_SO : ScriptableObject
{
    public string characterName;
    public Sprite characterSprite;

    public enum PrimaryStat { Int, Str, Dex };
    public int characterBaseHealth;
    [Range(1, 10)]
    public int[] baseStats = new int[4];

    public PrimaryStat primaryStat;

    
}
