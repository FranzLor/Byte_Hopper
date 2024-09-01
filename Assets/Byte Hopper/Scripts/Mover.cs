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


}
