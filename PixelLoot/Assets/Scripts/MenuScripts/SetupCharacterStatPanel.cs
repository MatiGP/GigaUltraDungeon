using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SetupCharacterStatPanel : MonoBehaviour
{
    public PlayableChar_SO[] playableChars;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI characterBaseHealth;
    public TextMeshProUGUI characterBaseMana;
    public TextMeshProUGUI characterInt;
    public TextMeshProUGUI characterStr;
    public TextMeshProUGUI characterDex;
    public TextMeshProUGUI characterVit;
    public TextMeshProUGUI characterScaling;
    

    public void setupWindow(int chosen)
    {
        PlayerPrefs.SetInt("selectedChar", chosen);
        chosen -= 1;
        characterName.text = playableChars[chosen].characterName;
        characterBaseHealth.text = playableChars[chosen].characterBaseHealth.ToString();
        characterBaseMana.text = playableChars[chosen].characterBaseMana.ToString();
        characterInt.text = playableChars[chosen].baseStats[0].ToString();
        characterStr.text = playableChars[chosen].baseStats[1].ToString();
        characterDex.text = playableChars[chosen].baseStats[2].ToString();
        characterVit.text = playableChars[chosen].baseStats[3].ToString();
        characterScaling.text = playableChars[chosen].primaryStat.ToString();
        
    }
}
