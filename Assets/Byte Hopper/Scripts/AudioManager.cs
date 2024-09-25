using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    // ui
    public Slider musicSlider, sfxSlider;

    public static AudioManager instance;

    private void Awake()
    {
        // singleton
        // create only one instance of audio manager
        if (instance == null)
        {
            instance = this;
            // carry audio manager through scenes
            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            LoadVolumes();
        }
        else
        {
            SetMusicVolume();
        }

        if (PlayerPrefs.HasKey("SFX"))
        {
            LoadVolumes();
        }
        else
        {
            SetSFXVolume();
        }

        SetMusicVolume();
        SetSFXVolume();

        PlayMusic("Game");

    }

    public void PlayMusic(string name)
    {
        Sound s = System.Array.Find(musicSounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = System.Array.Find(sfxSounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
    }

    public void StopMusic(string name)
    {
        Sound s = System.Array.Find(musicSounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        // Check if the currently playing clip is the one you want to stop
        if (musicSource.clip == s.clip && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    public void StopSFX(string name)
    {
        Sound s = System.Array.Find(sfxSounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (sfxSource.clip == s.clip && sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("Music", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("SFX", volume);
    }

    public void LoadVolumes()
    {
        musicSlider.value = PlayerPrefs.GetFloat("Music");
        sfxSlider.value = PlayerPrefs.GetFloat("SFX");

        SetMusicVolume();
        SetSFXVolume();
    }


}
