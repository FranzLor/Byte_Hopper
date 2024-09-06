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

    public ParticleSystem particle = null;

    public GameObject ghost = null;

    private Renderer renderer = null;
    private bool isVisible = false;

    void Start()
    {
        renderer = ghost.GetComponent<Renderer>();
    }

    void Update()
    {
        if (!Manager.instance.CanPlay()) return;

        if (!isDead)
        {
            CanIdle();
            CanMove();

            IsVisible();
        }
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
    }

    void CanMove()
    {
        if (isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Moving(new Vector3(transform.position.x, transform.position.y, transform.position.z + moveDistance));

                // updates for distance tracker - points
                SetMoveForwardState();

            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Moving(new Vector3(transform.position.x, transform.position.y, transform.position.z - moveDistance));
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Moving(new Vector3(transform.position.x - moveDistance, transform.position.y, transform.position.z));
            }
            else if (Input.GetKeyDown(KeyCode.D))
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
        Manager.instance.UpdateDistanceCount();
    }

    void IsVisible()
    {
        if (renderer.isVisible)
        {
            isVisible = true;
        }
        
        // moved out of camera view
        if (!renderer.isVisible && isVisible)
        {
            Debug.Log("Moved out of view");

            GotHit();
        }
    }

    public void GotHit()
    {
        isDead = true;

        // play death particle
        ParticleSystem.EmissionModule em = particle.emission;
        em.enabled = true;

        Manager.instance.GameOver();
    }
}
