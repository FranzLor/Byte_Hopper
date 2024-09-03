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

    }

    void Update()
    {

    }

    void SpawnItem()
    {
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
        // random position
        if (useSpawnPlacement)
        {
            return new Vector3(
                Random.Range(spawnLeftPosition, spawnRightPosition),
                startPosition.position.y,
                startPosition.position.z
            );
        }
        // actual starting position
        else
        {
            return startPosition.position;
        }
    }

}
