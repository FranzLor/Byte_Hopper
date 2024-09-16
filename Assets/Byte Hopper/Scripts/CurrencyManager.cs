using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    private int currentCoins = 0;
    private const string CoinsKey = "PlayerCoins";

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
        LoadCoins();
    }

    public void AddCoins(int value)
    {
        currentCoins += value;
        SaveCoins();
    }

    public bool SpendCoins(int value)
    {
        if (currentCoins >= value)
        {
            currentCoins -= value;
            SaveCoins();
            return true;
        }
        else
        {
            Debug.Log("Not Enough Coins");

            // todo add a popup or something
            return false;
        }
    }

    public int GetCoinBalance()
    {
        return currentCoins;
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetInt(CoinsKey, currentCoins);
        PlayerPrefs.Save();
    }

    private void LoadCoins()
    {
        currentCoins = PlayerPrefs.GetInt(CoinsKey, 0);
    }


}
