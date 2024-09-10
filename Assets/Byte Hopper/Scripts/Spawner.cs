using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform startPosition = null;

    public float delayMin = 2.0f;
    public float delayMax = 5.0f;

    public float speedMin = 1.0f;
    public float speedMax = 6.0f;

    public bool useSpawnPlacement = false;
    public int spawnCountMin = 4;
    public int spawnCountMax = 18;

    private float lastTime = 0.0f;
    private float delayTime = 0.0f;

    private float speed = 0.0f;

    // spawned objects
    [HideInInspector] public GameObject item = null;

    [HideInInspector] public bool goLeft = false;

    [HideInInspector] public float spawnLeftPosition = 0.0f;

    [HideInInspector] public float spawnRightPosition = 0.0f;

    void Start()
    {
        if (useSpawnPlacement)
        {
            // static objs
            int spawnCount = Random.Range(spawnCountMin, spawnCountMax);

            for (int i = 0; i < spawnCount; i++)
            {
                SpawnItem();
            }
        }
        else
        {
            // moving objs
            speed = Random.Range(speedMin, speedMax);
        }
    }

    void Update()
    {
        if (useSpawnPlacement) return;

        if (Time.time > lastTime + delayTime)
        {
            lastTime = Time.time;

            delayTime = Random.Range(delayMin, delayMax);

            SpawnItem();
        }
    }

    void SpawnItem()
    {
        // TODO remove
        Debug.Log("Object Spawned");

        // make sure to turn off objs in inspector (spawnLeft/spawnRight objs)
        // since it randomly chooses which side to spawn
        GameObject obj = Instantiate(item) as GameObject;

        obj.transform.position = GetSpawnPosition();

        float direction = 0.0f;

        if (goLeft)
        {
            direction = 180.0f;
        }

        if (!useSpawnPlacement)
        {
            obj.GetComponent<Mover>().speed = speed;
        }

        obj.transform.rotation = obj.transform.rotation * Quaternion.Euler(0, direction, 0);
    }

    Vector3 GetSpawnPosition()
    {
        // positions are even integers
        float x = Mathf.Round(Random.Range(spawnLeftPosition, spawnRightPosition));
        float z = Mathf.Round(startPosition.position.z);

        // adjust to nearest even number
        // since player moves at 2 units
        x = Mathf.Round(x / 2) * 2;
        z = Mathf.Round(z / 2) * 2;


        if (useSpawnPlacement)
        {
            return new Vector3(x, startPosition.position.y, z);
        }
        else
        {
            return startPosition.position;
        }
    }

}
