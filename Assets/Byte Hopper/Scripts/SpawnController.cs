using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public bool goLeft = false;
    public bool goRight = false;
    public bool goBoth = false;

    public List<GameObject> items = new List<GameObject>();

    public List<Spawner> spawnerLeft = new List<Spawner>();
    public List<Spawner> spawnerRight = new List<Spawner>();

    void Start()
    {
        // create random ID for item
        int itemID = Random.Range(0, items.Count);

        GameObject item = items[itemID];

        int direction = Random.Range(0, 2);

        if (goBoth)
        {
            // fixes the issue of spawning items on only half of the side
            goLeft = true;
            goRight = true;
        }
        else if (direction > 0)
        {
            goLeft = false;
            goRight = true;
        }
        else
        {
            goLeft = true;
            goRight = false;
        }

        // assign item to based off spawner
        for (int i = 0; i < spawnerLeft.Count; i++)
        {
            spawnerLeft[i].item = item;
            spawnerLeft[i].goLeft = goLeft;
            spawnerLeft[i].gameObject.SetActive(goLeft);
            spawnerLeft[i].spawnLeftPosition = spawnerLeft[i].transform.position.x;
        }

        for (int i = 0; i < spawnerRight.Count; i++)
        {
            spawnerRight[i].item = item;
            spawnerRight[i].goLeft = goLeft;
            spawnerRight[i].gameObject.SetActive(goRight);
            spawnerRight[i].spawnRightPosition = spawnerRight[i].transform.position.x;

        }
    }
}
