using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> platform = new List<GameObject>();
    // track platform height to account for different platform heights
    public List<float> height = new List<float>();

    private int randomRange = 0;
    private float lastPosition = 0;
    private float lastScale = 0;

    public void randomGenerator()
    {
        randomRange = Random.Range(0, platform.Count);

        for (int i = 0; i < platform.Count; i++)
        {
            CreateLevelObject(platform[i], height[i], i);
        }
    }

    public void CreateLevelObject(GameObject obj, float height, int value)
    {
        if (randomRange == value)
        {
            GameObject gameObj = Instantiate(obj) as GameObject;

            // get half value to center the object
            float offset = lastPosition + (lastScale * 0.5f);

            offset += (gameObj.transform.localScale.z) * 0.5f;
            Vector3 position = new Vector3(0, height, offset);

            gameObj.transform.position = position;
            lastPosition = gameObj.transform.position.z;
            lastScale = gameObj.transform.localScale.z;

            gameObj.transform.parent = this.transform;
        }
    }
}
 