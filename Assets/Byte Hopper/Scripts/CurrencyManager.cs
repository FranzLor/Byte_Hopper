using System.Collections;
using System.Collections.Generic;
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

        currentCoins = PlayerPrefs.GetInt("Coins", 0);
    }

    //private void Start()
    //{
    //    LoadCoins();
    //}

    public int GetCoinBalance()
    {
        return currentCoins;
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount;
        PlayerPrefs.SetInt("Coins", currentCoins);
    }

    public void SpendCoins(int amount)
    {
        if (currentCoins >= amount)
        {
            currentCoins -= amount;
            PlayerPrefs.SetInt("Coins", currentCoins);
        }
        else
        {
            Debug.Log("Not enough coins to spend.");
        }
    }

    //public int GetCoinBalance()
    //{
    //    return currentCoins;
    //}

    //private void SaveCoins()
    //{
    //    PlayerPrefs.SetInt(CoinsKey, currentCoins);
    //    PlayerPrefs.Save();
    //}

    //private void LoadCoins()
    //{
    //    currentCoins = PlayerPrefs.GetInt(CoinsKey, 0);
    //}


}
