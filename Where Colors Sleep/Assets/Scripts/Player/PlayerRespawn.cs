using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector2 currentCheckpoint;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentCheckpoint = transform.position;
    }

    public void UpdateCheckpoint(Vector2 newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
    }

    public void Respawn()
    {
        transform.position = currentCheckpoint;
        if (rb != null) rb.velocity = Vector2.zero;
    }
}
