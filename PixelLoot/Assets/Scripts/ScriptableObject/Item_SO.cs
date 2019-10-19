using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class Item_SO : ScriptableObject
{
    public Sprite itemIcon;
    public string itemName;

    public virtual void Use() { }

}
