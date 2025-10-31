using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector2 currentCheckpoint;
    private PlayerHealth playerHealth;
    private Rigidbody2D rb;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();

        
        currentCheckpoint = transform.position;
    }

    public void UpdateCheckpoint(Vector2 newPosition)
    {
        currentCheckpoint = newPosition;
    }

    public void RespawnPlayer()
    {
        
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        
        transform.position = currentCheckpoint;

        
        if (playerHealth != null)
            playerHealth.ResetHealth();
    }
}
