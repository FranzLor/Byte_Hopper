using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public PlayerController playerController = null;
    private Animator animator = null;

    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {

        if (playerController.isDead)
        {
            animator.SetBool("Dead", true);
            return;
        }

        if (playerController.isJumping)
        {
            animator.SetBool("Fly Forward 02", true);
        }
        else
        {
            animator.SetBool("Fly Forward 02", false);
        }

        if (!playerController.isIdle) return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }
}
