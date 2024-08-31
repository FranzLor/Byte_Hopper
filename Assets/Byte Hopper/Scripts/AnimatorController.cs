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
        }

        if (playerController.jumpStart)
        {
            animator.SetBool("Fly Forward 02 In Place", true);
        }
        else if (playerController.isJumping)
        {
            animator.SetBool("Fly Forward", true);
        }
        else
        {
            animator.SetBool("Fly Forward 02 In Place", false);
            animator.SetBool("Fly Forward", false);
        }
    }
}
