using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;


    //// main audio sources
    //public AudioSource musicSource = null;
    //public AudioSource sfxSource = null;

    //// background sfx
    //public AudioClip ambience = null;

    //// UI SFX
    //public AudioClip buttonSFX = null;

    //// player sfx
    //public AudioClip idleSFX1 = null;
    //public AudioClip idleSFX2 = null;
    //public AudioClip hitSFX = null;
    //public AudioClip splashSFX = null;
    //public AudioClip electricSFX = null;
    //public AudioClip coinSFX = null;
    //public AudioClip jumpSFX = null;

    //public static AudioManager instance;

    //private void Awake()
    //{
    //    // singleton
    //    // creates only one instance of the audio manager
    //    if (instance == null)
    //    {
    //        instance = this;
    //        // carries audio manager through scenes
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
        
    //}

    //public void Start()
    //{
    //    musicSource.clip = ambience;
    //    musicSource.Play();
    //}


    //public void PlaySFX(AudioClip clip)
    //{
    //    sfxSource.PlayOneShot(clip);
    //}
}
