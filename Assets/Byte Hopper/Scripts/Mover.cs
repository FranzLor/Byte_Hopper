using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 1.0f;
    public float moveDirection = 0.0f;

    public bool parentOnTrigger = true;
    public bool hitBoxTrigger = false;

    public GameObject moverObject = null;

    private Renderer renderer = null;
    private bool isVisible = false;

    void Start()
    {
        renderer = moverObject.GetComponent<Renderer>();
    }

    void Update()
    {
        this.transform.Translate(0, 0, speed * Time.deltaTime);
        IsVisible();
    }

    void IsVisible()
    {
        if (renderer.isVisible)
        {
            isVisible = true;
        }
        if (!renderer.isVisible && isVisible)
        {
            Debug.Log("Object is no longer visible");

            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Enter Triggered");

            if (parentOnTrigger)
            {
                Debug.Log("Enter Parent Triggered");
                other.transform.parent = this.transform;
            }

            if (hitBoxTrigger)
            {
                Debug.Log("Enter HitBox Triggered");
                other.GetComponent<PlayerController>().GotHit();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (parentOnTrigger)
            {
                Debug.Log("Exit Parent Triggered");
                other.transform.parent = null;
            }
        }
    }
}
