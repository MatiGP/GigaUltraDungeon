using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public LevelFader fader;
    public GameObject tutorialPrompt;

    public void Play()
    {
        tutorialPrompt.SetActive(true);
    }

    public void Authors()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Play_Yes()
    {
        fader.FadeOut(1);
    }

    public void Play_No()
    {
        fader.FadeOut(2);
    }

    public void Play_Return()
    {
        tutorialPrompt.SetActive(false);
    }
}
