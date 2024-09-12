using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characterList;
    private int index = 0;


    void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected");

        // use child count to get the number of characters - more dynamic
        characterList = new GameObject[transform.childCount];

        // fill the array with the characters obj
        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        // disable character renderers
        foreach (GameObject go in characterList)
        {
            go.SetActive(false);
        }

        // toggle selected character
        if (characterList[index])
        {
            characterList[index].SetActive(true);
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

        // toggle on new character
        characterList[index].SetActive(true);
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

        // toggle on new character
        characterList[index].SetActive(true);
    }


    public void ConfirmButton()
    {
        // save the selected character
        PlayerPrefs.SetInt("CharacterSelected", index);

        SceneManager.LoadScene("ByteRunner");
    }

    public void CharacterSelectionButton()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

}
