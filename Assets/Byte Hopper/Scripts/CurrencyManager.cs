using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    private int currentCoins = 0;

    void Awake()
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

    void Start()
    {
        currentCoins = PlayerPrefs.GetInt("PlayerCoins", 0);
    }

    public int GetCoinBalance()
    {
        return currentCoins;
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount;
        PlayerPrefs.SetInt("PlayerCoins", currentCoins);
        PlayerPrefs.Save();

        // stat track
        StatTrackerManager.instance.AddCoins(amount);
    }

    public void SpendCoins(int amount)
    {
        if (currentCoins >= amount)
        {
            currentCoins -= amount;
            PlayerPrefs.SetInt("PlayerCoins", currentCoins);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("Not enough coins to spend.");
        }
    }

    public void ResetCoins()
    {
        currentCoins = 0;
        PlayerPrefs.SetInt("PlayerCoins", currentCoins);
        PlayerPrefs.Save();
    }

}
