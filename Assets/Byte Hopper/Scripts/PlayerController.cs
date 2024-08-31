using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveDistance = 1.0f;
    public float moveTime = 0.4f;
    public float colliderDistance = 1.0f;

    public bool isIdle = true;
    public bool isMoving = false;
    public bool isDead = false;
    public bool isJumping = false;
    public bool jumpStart = false;

    public ParticleSystem particle = null;

    public GameObject ghost = null;

    private Renderer renderer = null;
    private bool isVisible = false;
}
