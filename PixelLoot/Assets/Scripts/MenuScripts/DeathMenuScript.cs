using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuScript : MonoBehaviour
{
    public GameObject menu;
    public GameObject deathMenuUI;
    public static bool isPlayerDead;
    public LevelFader fader;

    private void Update()
    {
        if (isPlayerDead)
        {
            deathMenuUI.SetActive(true);
        }
        else
        {
            deathMenuUI.SetActive(false);
        }
    }

    public void disableMenu()
    {
        PauseScirpt.isGamePaused = false;
    }

    public void RestartGame()
    {
        isPlayerDead = false;        
        PlayerStats.isPlayerAlive = true;
        PlayerStats.instance.playerSAI.itemsInInventory = new Item_SO[5];
        fader.FadeOut(SceneManager.GetActiveScene().buildIndex);
    }

    public void mainMenu()
    {
        isPlayerDead = false;       
        PlayerStats.isPlayerAlive = true;
        fader.FadeOut(2);
    }

    public void leaveGame()
    {
        Application.Quit();
    }
}
