using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuScript : MonoBehaviour
{
    public GameObject menu;
    public GameObject deathMenuUI;
    public static bool isPlayerDead;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void mainMenu()
    {
        isPlayerDead = false;       
        PlayerStats.isPlayerAlive = true;
        SceneManager.LoadScene(1);
    }

    public void leaveGame()
    {
        Application.Quit();
    }
}
