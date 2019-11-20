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
        Walker.hasPlayerBeenInstantiated = false;
        PlayerStats.isPlayerAlive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void mainMenu()
    {
        isPlayerDead = false;
        Walker.hasPlayerBeenInstantiated = false;
        PlayerStats.isPlayerAlive = true;
        SceneManager.LoadScene(0);
    }

    public void leaveGame()
    {
        Application.Quit();
    }
}
