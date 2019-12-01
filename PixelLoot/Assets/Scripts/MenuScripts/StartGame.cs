using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public PlayerStatsAndItems playerSAI;
    public SetupCharacterStatPanel statPanel;


    public void LoadLevel()
    {
        playerSAI.currentHealth = statPanel.playableChars[PlayerPrefs.GetInt("selectedChar")-1].characterBaseHealth + statPanel.playableChars[PlayerPrefs.GetInt("selectedChar") - 1].baseStats[3];
        playerSAI.currentMana = statPanel.playableChars[PlayerPrefs.GetInt("selectedChar")-1].characterBaseMana + statPanel.playableChars[PlayerPrefs.GetInt("selectedChar") - 1].baseStats[0];
        playerSAI.itemsInInventory = new Item_SO[5];
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(2);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;

            yield return null;
        }
    }
}
