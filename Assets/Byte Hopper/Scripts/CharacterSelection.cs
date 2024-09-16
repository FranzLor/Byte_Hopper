using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characterList;
    private int index = 0;

    // prices
    public int[] skinPrices;
    private bool[] skinUnlocked;

    public Text coinText;
    public Text unlockCostText;


    void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected");

        UpdateCoinUI();

        // use child count to get the number of characters - more dynamic
        characterList = new GameObject[transform.childCount];

        // fill the array with the characters obj
        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        // disable all characters at the start
        foreach (GameObject go in characterList)
        {
            go.SetActive(false);
        }

        // activate the selected character
        if (characterList[index])
        {
            characterList[index].SetActive(true);
        }

        // initialize skin unlocks
        skinUnlocked = new bool[characterList.Length];

        LoadSkinUnlocks();

        UpdateCoinUI();
        UpdateCharacterSelection();
    }

    void UpdateCharacterSelection()
    {
        // disable all character
        foreach (GameObject go in characterList)
        {
            go.SetActive(false);
        }

        // activate selected character
        if (characterList[index])
        {
            characterList[index].SetActive(true);
        }

        if (skinUnlocked[index])
        {
            unlockCostText.gameObject.SetActive(false);
        }
        else
        {
            unlockCostText.gameObject.SetActive(true);
            unlockCostText.text = skinPrices[index].ToString();
        }
    }

    public void ToggleLeft()
    {
        // toggle off current character
        characterList[index].SetActive(false);

        index--;

        if (index < 0)
        {
            index = characterList.Length - 1;
        }

        UpdateCharacterSelection();

        // toggle on new character
        //characterList[index].SetActive(true);
    }

    public void ToggleRight()
    {
        // toggle off current character
        characterList[index].SetActive(false);

        index++;

        if (index == characterList.Length)
        {
            index = 0;
        }

        UpdateCharacterSelection();

        // toggle on new character
        //characterList[index].SetActive(true);
    }


    public void ConfirmButton()
    {
        if (skinUnlocked[index])
        {
            PlayerPrefs.SetInt("CharacterSelected", index);

            SceneManager.LoadScene("ByteRunner");
        }
        // save the selected character
        //PlayerPrefs.SetInt("CharacterSelected", index);

        //SceneManager.LoadScene("ByteRunner");
    }

    void PurchaseSkin()
    {
        if (!skinUnlocked[index] && CurrencyManager.instance.GetCoinBalance() >= skinPrices[index])
        {
            CurrencyManager.instance.SpendCoins(skinPrices[index]);
            skinUnlocked[index] = true;
            SaveSkinUnlocks();
            UpdateCharacterSelection();
        }
    }

    void LoadSkinUnlocks()
    {
        for (int i = 0; i < skinUnlocked.Length; i++)
        {
            skinUnlocked[i] = PlayerPrefs.GetInt("SkinUnlocked_" + i, i == 0 ? 1 : 0) == 1;
        }
    }

    void SaveSkinUnlocks()
    {
        for (int i = 0; i < skinUnlocked.Length; i++)
        {
            PlayerPrefs.SetInt("SkinUnlocked_" + i, skinUnlocked[i] ? 1 : 0);
        }

    }

    void UpdateCoinUI()
    {
        coinText.text = CurrencyManager.instance.GetCoinBalance().ToString();
    }

    public void CharacterSelectionButton()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

}
