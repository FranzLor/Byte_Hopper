using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveDistance = 2.0f;
    public float moveTime = 0.24f;
    public float colliderDistanceCheck = 2.0f;

    // distance tracking
    private HashSet<Vector3> visitedPositions = new HashSet<Vector3>();

    public bool isIdle = true;
    public bool isMoving = false;
    public bool isDead = false;
    public bool isJumping = false;

    public ParticleSystem mainParticle = null;
    public ParticleSystem deathParticle = null;

    public ParticleSystem splashParticle = null;
    public bool parentedToObject = false;

    public GameObject ghost = null;

    private Renderer renderer = null;
    private bool isVisible = false;

    public bool enableAngleCheck = true;
    public float angleCheck = 0.25f;
    public float angleCheckDistance = 0.5f;

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
                if (enableAngleCheck)
                {
                    CheckIfCanMoveAngle();

                    //PlayAudioClip(audioIdle1);
                }
                else
                {
                    CheckIfCanMoveSingleRay();
                }
            }
        }
    }


    void CheckIfCanMoveSingleRay()
    {
        // define the forward direction for the raycast
        Vector3 rayDirection = this.transform.forward;

        RaycastHit hit;
        // cast a ray forward to check for obstacles
        bool isHit = Physics.Raycast(this.transform.position, rayDirection, out hit, colliderDistanceCheck);

        // visualize the ray in the scene view for debugging
        Debug.DrawRay(this.transform.position, rayDirection * colliderDistanceCheck, Color.red, 1.0f);

        if (!isHit)
        {
            // No obstacle in the way, player can move
            SetMove();
        }
        else
        {
            // obstacle detected, check if it's tagged as a "collider" (like a tree)
            if (hit.collider != null && hit.collider.tag == "collider")
            {
                Debug.Log("Hit Collider - Cannot Move: " + hit.collider.name);
            }
            else
            {
                // something is hit, but it's not a blocking object
                SetMove();
            }
        }
    }

    void CheckIfCanMoveAngle()
    {
        RaycastHit hit;
        RaycastHit hitLeft;
        RaycastHit hitRight;

        Physics.Raycast(this.transform.position, -ghost.transform.up, out hit, colliderDistanceCheck);
        Physics.Raycast(this.transform.position, -ghost.transform.up + new Vector3(angleCheck, 0, 0), out hitLeft, colliderDistanceCheck + angleCheckDistance);
        Physics.Raycast(this.transform.position, -ghost.transform.up + new Vector3(-angleCheck, 0, 0), out hitRight, colliderDistanceCheck + angleCheckDistance);

        Debug.DrawRay(this.transform.position, -ghost.transform.up * colliderDistanceCheck, Color.red, 2);
        Debug.DrawRay(this.transform.position, (-ghost.transform.up + new Vector3(angleCheck, 0, 0)) * (colliderDistanceCheck + angleCheckDistance), Color.green, 2);
        Debug.DrawRay(this.transform.position, (-ghost.transform.up + new Vector3(-angleCheck, 0, 0)) * (colliderDistanceCheck + angleCheckDistance), Color.blue, 2);

        if (hit.collider == null && hitLeft.collider == null && hitRight.collider == null)
        {
            // move to area if no collisions
            SetMove();
        }
        else
        {
            if (hit.collider != null && hit.collider.tag == "collider")
            {
                Debug.Log("Hit Center with Collider");

                isIdle = true;
            }
            else if (hitLeft.collider != null && hitLeft.collider.tag == "collider")
            {
                Debug.Log("Hit Left with Collider");
                isIdle = true;

            }
            else if (hitRight.collider != null && hitRight.collider.tag == "collider")
            {
                Debug.Log("Hit Right with Collider");
                isIdle = true;

            }
            // if no colliders, move to area
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
        if (isDead) return;

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

        AudioManager.instance.PlaySFX("Jump");

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
        // store current position
        Vector3 currentPosition = transform.position;

        // fixes issue with counting 1 distance every forward movement
        // check if position has been visited
        if (!visitedPositions.Contains(currentPosition))
        {
            visitedPositions.Add(currentPosition);
            Manager.instance.AddDistance(1);
        }


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
        ParticleSystem.EmissionModule em = deathParticle.emission;
        em.enabled = true;

        AudioManager.instance.PlaySFX("Hit");

        ghost.SetActive(false);
        // hide main particle
        ParticleSystem.EmissionModule emissionModule = mainParticle.emission;
        emissionModule.enabled = false;

        // stat track
        StatTrackerManager.instance.AddDeath();

        Manager.instance.GameOver();
    }

    public void InWater()
    {
        isDead = true;

        // play death particle
        ParticleSystem.EmissionModule em = splashParticle.emission;
        em.enabled = true;

        AudioManager.instance.PlaySFX("Splash");

        // hides player - fish in the water
        ghost.SetActive(false);
        // hide main particle
        ParticleSystem.EmissionModule emissionModule = mainParticle.emission;
        emissionModule.enabled = false;

        // stat track
        StatTrackerManager.instance.AddDeath();

        Manager.instance.GameOver();
    }

    public void Electrified()
    {
        isDead = true;

        // play death particle

        AudioManager.instance.PlaySFX("Electrified");

        ghost.SetActive(false);
        ParticleSystem.EmissionModule emissionModule = mainParticle.emission;
        emissionModule.enabled = false;

        // stat track
        StatTrackerManager.instance.AddDeath();

        Manager.instance.GameOver();
    }

    //public void PlayAudioClip(AudioClip clip)
    //{
    //    this.GetComponent<AudioSource>().PlayOneShot(clip);
    //}
}
