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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void leaveGame()
    {
        Application.Quit();
    }
}
