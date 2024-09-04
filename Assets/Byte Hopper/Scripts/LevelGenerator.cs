using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameOject> platform = new List<GameObject>();
    // track platform height to account for different platform heights
    public List<float> height = new List<float>();

    private int randomRange = 0;
    private float lastPosition = 0;
    private float lastScale = 0;

    public void randomGenerator()
    {

    }

    public void CreateLevelObject(GameObject obj, float height, int value)
    {

    }
}
 