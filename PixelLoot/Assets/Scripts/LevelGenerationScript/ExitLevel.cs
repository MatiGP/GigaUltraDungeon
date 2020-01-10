using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    public LevelFader fader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats.instance.SaveState();
            
            PlayerStats.instance.GetComponentInChildren<CharacterStatsUI>().SetFloorText(PlayerStats.instance.playerSAI.currentLevel++);
            if(PlayerStats.instance.playerSAI.currentLevel == 2)
            {
                fader.FadeOut(4);
                PlayerStats.instance.GetComponentInChildren<CharacterStatsUI>().SetFloorText("BOSS");
            }
            else
            {
                fader.FadeOut(3);
            }
            
            
        }
    }
}
