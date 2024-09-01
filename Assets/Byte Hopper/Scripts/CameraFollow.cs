using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool autoMove = true;

    public GameObject player = null;

    public Vector3 offset = new Vector3(0, 0, 0);
    Vector3 depth = Vector3.zero;
    Vector3 position = Vector3.zero;

    void Update()
    {

    }
}
