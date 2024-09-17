using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebuggerTest : MonoBehaviour
{
    public Button addCoinsButton;
    public Button resetCoinsButton;
    public Button resetUnlocksButton;

    void Start()
    {
        addCoinsButton.onClick.AddListener(() => AddCoinsForTesting());
        resetCoinsButton.onClick.AddListener(() => ResetCoinsForTesting());
        resetUnlocksButton.onClick.AddListener(() => ResetUnlocksForTesting());
    }

    void AddCoinsForTesting()
    {
        CurrencyManager.instance.AddCoins(20);
    }

    void ResetCoinsForTesting()
    {
        CurrencyManager.instance.ResetCoins();
    }

    void ResetUnlocksForTesting()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("CharacterSelected"); i++)
        {
            PlayerPrefs.SetInt("SkinUnlocked_" + i, 0);
        }
        PlayerPrefs.Save();
    }
}
