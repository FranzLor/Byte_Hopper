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

    public GameObject guiGameOver = null;

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


    void Start()
    {
        // TODO: level gen

    }

    public void UpdateCoinCount(int value)
    {
        Debug.Log("Coin Collected: " + value);

        currentCoins += value;

        coinCounter.text = currentCoins.ToString();
    }

    public void UpdateDistanceCount()
    {
        Debug.Log("Moved");

        currentDistance += 1;

        distanceCounter.text = currentDistance.ToString();

        // TODO: generate new level piece here
    }

    public bool CanPlay()
    {
        return canPlay;
    }

    public void StartPlay()
    {
        canPlay = true;
    }

    public void GameOver()
    {
        // pauses camera at location
        camera.GetComponent<CameraShake>().Shake();
        camera.GetComponent<CameraFollow>().enabled = false;

        GUIGameOver();
    }

    void GUIGameOver()
    {
        Debug.Log("Game Over");

        guiGameOver.SetActive(true);
    }

    public void PlayAgain()
    {
        Scene scene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(scene.name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
