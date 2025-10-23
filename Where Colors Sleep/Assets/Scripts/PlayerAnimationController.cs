using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerController player;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        animator.SetBool("isWalking", player.GetIsWalking());
        animator.SetBool("isJumping", !player.GetIsGrounded());
    }
}
