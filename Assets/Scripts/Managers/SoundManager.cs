using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioClip backgroundMusic;


    [Range(0, 1)]
    public float musicVolume = 0.5f;

    AudioSource source;

    private void Start()
    {
        source = this.GetComponent<AudioSource>();
        PlayBackgroundMusic(backgroundMusic);
    }
    void PlayBackgroundMusic(AudioClip clip) {
        if (clip != null)
        {
            source.clip = clip;
            source.volume = musicVolume;
            source.Play();
        }
    }

    public void StopSound()
    {   if(source.clip != null)
        {
            source.Stop();
        }

    }
}
