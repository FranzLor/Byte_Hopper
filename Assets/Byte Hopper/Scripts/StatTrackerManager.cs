using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatTrackerManager : MonoBehaviour
{
    public static StatTrackerManager instance = null;

    // ref
    public TMP_Text highestDistanceText;
    public TMP_Text lifetimeDistanceText;
    public TMP_Text lifetimeCoinsText;
    public TMP_Text lifetimeDeathsText;

    public GameObject statTrackerScreen;
    public Button closeButton;
    public GameObject startScreen;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadStats();
        UpdateUI();
    }

    public void AddDistance(int distance)
    {
        int lifetimeDistance = PlayerPrefs.GetInt("LifetimeDistance", 0);

        lifetimeDistance += distance;
        PlayerPrefs.SetInt("LifetimeDistance", lifetimeDistance);

        CheckForHighestDistance(distance);
        UpdateUI();
    }

    public void AddCoins(int coins)
    {
        int lifetimeCoins = PlayerPrefs.GetInt("LifetimeCoins", 0);

        lifetimeCoins += coins;
        PlayerPrefs.SetInt("LifetimeCoins", lifetimeCoins);

        UpdateUI();
    }

    public void AddDeath()
    {
        int lifetimeDeaths = PlayerPrefs.GetInt("LifetimeDeaths", 0);
        lifetimeDeaths++;
        PlayerPrefs.SetInt("LifetimeDeaths", lifetimeDeaths);

        PlayerPrefs.Save();
        UpdateUI();
    }

    private void CheckForHighestDistance(int newDistance)
    {
        int highestDistance = PlayerPrefs.GetInt("HighestDistance", 0);
        if (newDistance > highestDistance)
        {
            PlayerPrefs.SetInt("HighestDistance", newDistance);
        }
    }

    private void LoadStats()
    {
        // load stats from player prefs
        // use playerprefs.getint
    }

    private void UpdateUI()
    {
        highestDistanceText.text = ScoreManager.instance.GetHighScore().ToString();

        lifetimeDistanceText.text = PlayerPrefs.GetInt("LifetimeDistance", 0).ToString();
        lifetimeCoinsText.text = PlayerPrefs.GetInt("LifetimeCoins", 0).ToString();
        lifetimeDeathsText.text = PlayerPrefs.GetInt("LifetimeDeaths", 0).ToString();
    }

    public void OpenStatTracker()
    {
        startScreen.SetActive(false);
        statTrackerScreen.SetActive(true);
    }

    public void CloseStatTracker()
    {
        statTrackerScreen.SetActive(false);
        startScreen.SetActive(true);
    }
}
