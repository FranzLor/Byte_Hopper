using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characterList;

    void Start()
    {
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

        // toggle default skin
        if (characterList[0])
        {
            characterList[0].SetActive(true);
        }
    }
}
