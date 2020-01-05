using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource projectileSoundEffect;
    public AudioSource onHitSoundEffect;
    public AudioSource tauntSoundEffect;
    public AudioSource deathSoundEffect;
    public AudioSource npcSoundEffects;
    public AudioSource uiSoundEffects;

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

    public void PlayProjectileSoundFX(AudioClip clip)
    {
        projectileSoundEffect.clip = clip;
        projectileSoundEffect.Play();
    }

    public void PlayOnHitSoundEffect(AudioClip clip)
    {
        onHitSoundEffect.clip = clip;
        onHitSoundEffect.Play();
    }

    public void PlayTauntSoundEffect(AudioClip clip)
    {
        tauntSoundEffect.clip = clip;
        tauntSoundEffect.Play();
    }

    public void PlayDeathSoundEffect(AudioClip clip)
    {
        deathSoundEffect.clip = clip;
        deathSoundEffect.Play();
    }

    public void PlayNpcSoudEffect(AudioClip clip)
    {
        npcSoundEffects.clip = clip;
        npcSoundEffects.Play();
    }

    public void PlayUiSoundEffect(AudioClip clip)
    {
        uiSoundEffects.clip = clip;
        uiSoundEffects.Play();
    }


}
