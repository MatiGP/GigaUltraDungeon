using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] LevelFader fader;   

    public void BackToMainMenu()
    {
        fader.FadeOut(0);
    }
}
