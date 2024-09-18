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
    public TMP_Text deathByCarsText;
    public TMP_Text deathByWaterText;
    public TMP_Text deathByMicrochipText;
    public TMP_Text deathByCameraText;

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

    public void AddDeath(string cause)
    {
        int lifetimeDeaths = PlayerPrefs.GetInt("LifetimeDeaths", 0);
        lifetimeDeaths++;
        PlayerPrefs.SetInt("LifetimeDeaths", lifetimeDeaths);

        switch (cause)
        {
            case "Car":
                int deathByCars = PlayerPrefs.GetInt("DeathByCars", 0);
                deathByCars++;
                PlayerPrefs.SetInt("DeathByCars", deathByCars);
                break;
            case "Water":
                int deathByWater = PlayerPrefs.GetInt("DeathByWater", 0);
                deathByWater++;
                PlayerPrefs.SetInt("DeathByWater", deathByWater);
                break;
            case "Microchip":
                int deathByMicrochip = PlayerPrefs.GetInt("DeathByMicrochip", 0);
                deathByMicrochip++;
                PlayerPrefs.SetInt("DeathByMicrochip", deathByMicrochip);
                break;
            case "Camera":
                int deathByCamera = PlayerPrefs.GetInt("DeathByCamera", 0);
                deathByCamera++;
                PlayerPrefs.SetInt("DeathByCamera", deathByCamera);
                break;
        }

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
        highestDistanceText.text = PlayerPrefs.GetInt("HighestDistance", 0).ToString();
        lifetimeDistanceText.text = PlayerPrefs.GetInt("LifetimeDistance", 0).ToString();
        lifetimeCoinsText.text = PlayerPrefs.GetInt("LifetimeCoins", 0).ToString();
        lifetimeDeathsText.text = PlayerPrefs.GetInt("LifetimeDeaths", 0).ToString();
        deathByCarsText.text = PlayerPrefs.GetInt("DeathByCars", 0).ToString();
        deathByWaterText.text = PlayerPrefs.GetInt("DeathByWater", 0).ToString();
        deathByMicrochipText.text = PlayerPrefs.GetInt("DeathByMicrochip", 0).ToString();
        deathByCameraText.text = PlayerPrefs.GetInt("DeathByCamera", 0).ToString();
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
