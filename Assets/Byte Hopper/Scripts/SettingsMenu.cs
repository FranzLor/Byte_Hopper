using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public GameObject startScreen;
    public GameObject settingsPanel;

    public Slider musicSlider, sfxSlider;

    void Awake()
    {
        LoadSettings();
    }

    public void OpenSettings()
    {
        // load settings before displaying the settings panel
        LoadSettings();

        musicSlider.value = PlayerPrefs.GetFloat("Music", 1);
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", 1);

        // show settings panel
        startScreen.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        PlayerPrefs.SetFloat("Music", musicSlider.value);
        PlayerPrefs.SetFloat("SFX", sfxSlider.value);
        PlayerPrefs.Save();

        // hide settings panel and return to the start screen
        startScreen.SetActive(true);
        settingsPanel.SetActive(false);
    }

    void LoadSettings()
    {
        // load saved volume
        if (PlayerPrefs.HasKey("Volume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume");
            audioMixer.SetFloat("Volume", savedVolume);
        }

        // load saved fullscreen preference
        if (PlayerPrefs.HasKey("FullScreen"))
        {
            bool isFullScreen = PlayerPrefs.GetInt("FullScreen") == 1;
            Screen.fullScreen = isFullScreen;
        }
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);

        // save the volume
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;

        // save fullscreen preference
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ToggleMusic()
    {
        AudioManager.instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.instance.ToggleSFX();
    }

}
