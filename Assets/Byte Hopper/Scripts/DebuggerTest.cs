using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebuggerTest : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    public Button addCoinsButton;
    [SerializeField]
    public Button resetCoinsButton;
    [SerializeField]
    public Button resetUnlocksButton;

#endif

    private CharacterSelection characterSelection;

    void Start()
    {
        // refer
        characterSelection = FindObjectOfType<CharacterSelection>();
        if (characterSelection == null)
        {
            Debug.LogError("CharacterSelection script not found.");
        }

        addCoinsButton.onClick.AddListener(() => AddCoinsForTesting());
        resetCoinsButton.onClick.AddListener(() => ResetCoinsForTesting());
        resetUnlocksButton.onClick.AddListener(() => ResetUnlocksForTesting());
    }

    void AddCoinsForTesting()
    {
        CurrencyManager.instance.AddCoins(20);

        Debug.Log("Coins added for testing.");

        // update UI
        if (characterSelection != null)
        {
            characterSelection.UpdateCoinUI();
        }
    }

    void ResetCoinsForTesting()
    {
        CurrencyManager.instance.ResetCoins();

        Debug.Log("Coins reset to zero.");

        // update the UI
        if (characterSelection != null)
        {
            characterSelection.UpdateCoinUI();
        }
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

        // reload skin unlocks and update the character selection UI
        if (characterSelection != null)
        {
            characterSelection.LoadSkinUnlocks();
            characterSelection.UpdateCharacterSelection();
        }
    }
}
