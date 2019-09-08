using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableChar_SO : ScriptableObject
{
    public string characterName;

    public int characterBaseHealth {
        get {
            return characterBaseHealth;
        }
        set {
            characterBaseHealth = value;
        }
    }
}
