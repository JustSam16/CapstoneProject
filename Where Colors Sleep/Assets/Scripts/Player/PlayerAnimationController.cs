using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerController player;
    private PlayerController_Water playerWater;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerController>();
        playerWater = GetComponent<PlayerController_Water>();
    }

    void Update()
    {
        if (animator == null) return;

        if (player != null)
        {
            animator.SetBool("isWalking", player.GetIsWalking());
            animator.SetBool("isJumping", !player.GetIsGrounded());
            animator.SetBool("isCrouching", Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow));
        }
        else if (playerWater != null)
        {
            animator.SetBool("isSwimming", true);
            animator.SetBool("isWalking", Input.GetAxisRaw("Horizontal") != 0);
            animator.SetBool("isCrouching", Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow));
            animator.SetBool("isJumping", false);
        }
    }

    public void PlayHurt()
    {
        if (animator != null)
            animator.SetTrigger("Hurt");
    }
}
