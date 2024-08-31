using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveDistance = 1.0f;
    public float moveTime = 0.4f;
    public float colliderDistanceCheck = 1.0f;

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
        CanIdle();
        CanMove();
    }

    void CanIdle()
    {
        if (isIdle)
        {
            // key down for animation to play first
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                CheckIfCanMove();
            }
        }
    }

    void CheckIfCanMove()
    {
        // raycast to check if can move - collider boxes in front of player
        RaycastHit hit;
        Physics.Raycast(this.transform.position, -ghost.transform.up, out hit, colliderDistanceCheck);

        Debug.DrawRay(this.transform.position, -ghost.transform.up * colliderDistanceCheck, Color.red, 2);

        if (hit.collider == null)
        {
            SetMove();
        }
        else
        {
            if (hit.collider.tag == "collider")
            {
                Debug.Log("Hit Collider - Cannot Move");
            }
            else
            {
                SetMove();
            }
        }
    }

    void SetMove()
    {
        Debug.Log("Hit Nothing - Can Move");
        isIdle = false;
        isMoving = true;
        jumpStart = true;
    }

    void CanMove()
    {
        if (isMoving)
        {
            // key up for animation - when key released, anim stops, then moves
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

        LeanTween.move(this.gameObject, position, moveTime).setOnComplete(MoveComplete);
    }

    void MoveComplete()
    {
        // called when tween is complete, resets states and locks
        isJumping = false;
        isIdle = true;
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
