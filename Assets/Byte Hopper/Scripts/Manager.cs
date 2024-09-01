using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public Text coinCounter = null;
    public Text distanceCounter = null;

    public Camera camera = null;

    private int currentCoins = 0;
    private int currentDistance = 0;

    private bool canPlay = false;

    public GameObject guiGmaeOver = null;

    // manager singleton
    private static Manager staticInstance = null;
    public static Manager instance
    {
        get
        {
            if (staticInstance == null)
            {
                staticInstance = FindObjectOfType(typeof(Manager)) as Manager;
            }

            return staticInstance;
        }
    }
}
