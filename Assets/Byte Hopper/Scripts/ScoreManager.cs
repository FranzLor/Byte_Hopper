using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private int currentDistance = 0;
    private int highScore = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }


    public void AddDistance(int distance)
    {
        currentDistance += distance;

        if (currentDistance > highScore)
        {
            highScore = currentDistance;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }

    public int GetCurrentDistance()
    {
        return currentDistance;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public void ResetDistance()
    {
        currentDistance = 0;
    }
}

    
