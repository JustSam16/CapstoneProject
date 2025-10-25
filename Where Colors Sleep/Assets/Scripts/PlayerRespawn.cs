using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector2 respawnPoint;
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        respawnPoint = transform.position; 
    }

    public void SetCheckpoint(Vector2 newPosition)
    {
        respawnPoint = newPosition;
        Debug.Log("Nuovo checkpoint impostato: " + respawnPoint);
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
        playerHealth.ResetHealth();
        Debug.Log("Player respawnato a " + respawnPoint);
    }
}
