using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource music;
    public AudioSource soundFX;
    

    public AudioClip coinDrop;

    public static SoundManager instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(AudioClip clip)
    {
        music.clip = clip;
        music.Play();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        soundFX.clip = clip;
        soundFX.Play();
    }

    public void PlayCoinDropSound()
    {
        soundFX.clip = coinDrop;
        soundFX.Play();
    }
    
}
