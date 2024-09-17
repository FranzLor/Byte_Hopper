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
        // reset all skins by iterating through the same number of skins as in the CharacterSelection script
        int totalSkins = transform.childCount;

        for (int i = 0; i < totalSkins; i++)
        {
            if (i == 0)
            {
                // always unlock the first/default skin
                PlayerPrefs.SetInt("SkinUnlocked_" + i, 1);  
            }
            else
            {
                PlayerPrefs.SetInt("SkinUnlocked_" + i, 0);
            }
        }

        PlayerPrefs.Save();

        Debug.Log("All skins have been reset to locked.");

        // reload skin unlocks to update the UI
        CharacterSelection characterSelection = FindObjectOfType<CharacterSelection>();
        if (characterSelection != null)
        {
            characterSelection.LoadSkinUnlocks();
            characterSelection.UpdateCharacterSelection();
        }
        else
        {
            Debug.LogError("CharacterSelection script not found.");
        }
    }
}
