using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public PlayerStatsAndItems playerSAI;
    public SetupCharacterStatPanel statPanel;
    public LevelFader fader;


    public void LoadLevel()
    {
        playerSAI.currentHealth = statPanel.playableChars[PlayerPrefs.GetInt("selectedChar")-1].characterBaseHealth + statPanel.playableChars[PlayerPrefs.GetInt("selectedChar") - 1].baseStats[3];
        playerSAI.currentMana = statPanel.playableChars[PlayerPrefs.GetInt("selectedChar")-1].characterBaseMana + statPanel.playableChars[PlayerPrefs.GetInt("selectedChar") - 1].baseStats[0];
        playerSAI.itemsInInventory = new Item_SO[5];
        playerSAI.currentLevel = 1;
        fader.FadeOut(3);
    }
  
}
