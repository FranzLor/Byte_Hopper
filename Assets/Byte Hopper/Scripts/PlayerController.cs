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

    void Start()
    {

    }

    void Update()
    {
        CanMove();
    }

    void CanIdle()
    {

    }

    void CheckIfCanMove()
    {

    }

    void SetMove()
    {

    }

    void CanMove()
    {
        if (isMoving)
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                Moving(new Vector3(transform.position.x, transform.position.y, transform.position.z + moveDistance));

                // updates for distance tracker - points
                SetMoveForwardState();

            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                Moving(new Vector3(transform.position.x, transform.position.y, transform.position.z - moveDistance));
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                Moving(new Vector3(transform.position.x - moveDistance, transform.position.y, transform.position.z));
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                Moving(new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z));
            }
        }
        
    }

    void Moving(Vector3 position)
    {
        // resets locking system
        isIdle = false;
        isMoving = false;
        
        isJumping = true;
        jumpStart = false;

        LeanTween.move(this.gameObject, position, moveTime);
    }

    void MoveComplete()
    {

    }

    void SetMoveForwardState()
    {

    }

    void IsVisible()
    {

    }

    public void GotHit()
    {

    }
}
