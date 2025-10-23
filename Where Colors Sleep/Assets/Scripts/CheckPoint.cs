using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool activated;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;
        if (other.CompareTag("Player"))
        {
            activated = true;
            GameManager.instance.SetCheckpoint(transform.position);

            if (animator != null)
                animator.SetTrigger("Activate");

            Debug.Log("Checkpoint attivato!");
        }
    }
}
