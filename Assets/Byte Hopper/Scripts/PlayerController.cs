using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveDistance = 2.0f;
    public float moveTime = 0.24f;
    public float colliderDistanceCheck = 2.0f;

    public bool isIdle = true;
    public bool isMoving = false;
    public bool isDead = false;
    public bool isJumping = false;

    public ParticleSystem particle = null;

    public ParticleSystem splashParticle = null;
    public bool parentedToObject = false;

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
        // Define the forward direction for the raycast
        Vector3 rayDirection = this.transform.forward;

        RaycastHit hit;
        // Cast a ray forward to check for obstacles
        bool isHit = Physics.Raycast(this.transform.position, rayDirection, out hit, colliderDistanceCheck);

        // Visualize the ray in the scene view for debugging
        Debug.DrawRay(this.transform.position, rayDirection * colliderDistanceCheck, Color.red, 1.0f);

        if (!isHit)
        {
            // No obstacle in the way, player can move
            SetMove();
        }
        else
        {
            // Obstacle detected, check if it's tagged as a "collider" (like a tree)
            if (hit.collider != null && hit.collider.tag == "collider")
            {
                Debug.Log("Hit Collider - Cannot Move: " + hit.collider.name);
                // Player cannot move, obstacle detected
            }
            else
            {
                // Something is hit, but it's not a blocking object
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

        ghost.SetActive(false);

        Manager.instance.GameOver();
    }

    public void InWater()
    {
        isDead = true;

        // play death particle
        ParticleSystem.EmissionModule em = splashParticle.emission;
        em.enabled = true;

        // hides player - fish in the water
        ghost.SetActive(false);

        Manager.instance.GameOver();
    }

    public void Electrified()
    {
        isDead = true;

        // play death particle

        ghost.SetActive(false);
        Manager.instance.GameOver();
    }
}
