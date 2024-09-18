using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
}
