using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats.instance.SaveState();
            PlayerStats.instance.GetComponentInChildren<CharacterStatsUI>().SetFloorText(PlayerStats.instance.playerSAI.currentLevel++);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
