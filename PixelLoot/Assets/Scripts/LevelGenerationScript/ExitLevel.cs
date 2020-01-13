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
          
            if(PlayerStats.instance.playerSAI.currentLevel == 10)
            {
                fader.FadeOut(4);
            }
            else
            {
                PlayerStats.instance.playerSAI.currentLevel++;
                fader.FadeOut(3);
                
            }
            
            
        }
    }
}
