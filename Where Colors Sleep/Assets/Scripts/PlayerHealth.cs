using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Vita: " + currentHealth);

        if (currentHealth <= 0)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        currentHealth = maxHealth;

        if (GameManager.instance.HasCheckpoint())
            transform.position = GameManager.instance.GetCheckpointPosition();
        else
            transform.position = Vector3.zero; 

        Debug.Log("Respawn avvenuto");
    }
}
