using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelFader : MonoBehaviour
{
    private Animator animator;
    private int lvlToLoad;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void FadeOut(int lvlIndex)
    {
        lvlToLoad = lvlIndex;
        animator.SetTrigger("FadeOut");
    }
    public void OnFadeOutComplete()
    {
        SceneManager.LoadScene(lvlToLoad);
    }
    
}
