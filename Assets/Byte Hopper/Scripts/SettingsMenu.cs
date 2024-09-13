using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public GameObject startScreen;
    public GameObject settingsPanel;

    void Awake()
    {
        // Load settings when the game starts
        LoadSettings();
    }

    // Call this every time the settings menu is opened
    public void OpenSettings()
    {
        // Load settings before displaying the settings panel
        LoadSettings();

        // Show settings panel
        startScreen.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        // Hide settings panel and return to the start screen
        startScreen.SetActive(true);
        settingsPanel.SetActive(false);
    }

    void LoadSettings()
    {
        // Load saved volume
        if (PlayerPrefs.HasKey("Volume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume");
            audioMixer.SetFloat("Volume", savedVolume);
        }

        // Load saved fullscreen preference
        if (PlayerPrefs.HasKey("FullScreen"))
        {
            bool isFullScreen = PlayerPrefs.GetInt("FullScreen") == 1;
            Screen.fullScreen = isFullScreen;
        }
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);

        // Save the volume
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;

        // Save fullscreen preference
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }
}
