using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquiptedRelics : MonoBehaviour
{
    public Relic_SO[] wornRelics = new Relic_SO[4];
    public Button[] wornRelicsButtons;
    
    public void UpdateUI()
    {
       for(int i = 0; i < 4; i++)
        {
            wornRelicsButtons[i].image.sprite = wornRelics[i].itemIcon;
        }
    }
}
