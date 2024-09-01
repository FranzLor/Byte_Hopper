using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool autoMove = true;

    public GameObject player = null;

    public float speed = 0.8f;

    public Vector3 offset = new Vector3(4, 6, -4);
    Vector3 depth = Vector3.zero;
    Vector3 position = Vector3.zero;

    void Update()
    {
        if (!Manager.instance.CanPlay()) return;

        if (autoMove)
        {
            depth = this.gameObject.transform.position += new Vector3(0.0f, 0.0f, speed * Time.deltaTime);
            // lerp camera to player or target
            position = Vector3.Lerp(gameObject.transform.position, player.transform.position + offset, Time.deltaTime);
            // set camera position
            gameObject.transform.position = new Vector3(position.x, offset.y, depth.z);
        }
        else
        {
            position = Vector3.Lerp(gameObject.transform.position, player.transform.position + offset, Time.deltaTime);
            gameObject.transform.position = new Vector3(position.x, offset.y, position.z);
        }
    }
}
