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

    public LevelGenerator levelGenerator = null;
    public int levelCount = 10;

    //private int currentCoins = 0;
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
        for (int i = 0; i < levelCount; i++)
        {
            levelGenerator.randomGenerator();

        }

        // initialize manager and UI
        StartCoroutine(InitializeCoinUI());
    }

    private void UpdateCoinUI()
    {
        if (CurrencyManager.instance != null)
        {
            coinCounter.text = CurrencyManager.instance.GetCoinBalance().ToString();
        }
        else
        {
            Debug.LogWarning("CurrencyManager not found in scene");
        }
    }

    public void UpdateCoinCount(int value)
    {
        Debug.Log("Coin Collected: " + value);

        //currentCoins += value;

        CurrencyManager.instance.AddCoins(value);
        UpdateCoinUI();
    }

    IEnumerator InitializeCoinUI()
    {
        yield return new WaitUntil(() => CurrencyManager.instance != null);

        coinCounter.text = CurrencyManager.instance.GetCoinBalance().ToString();
    }

    public void UpdateDistanceCount()
    {
        Debug.Log("Moved");

        currentDistance += 1;

        distanceCounter.text = currentDistance.ToString();

        // when player moves up, generator piece, constantly generate pieces
        levelGenerator.randomGenerator();
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
