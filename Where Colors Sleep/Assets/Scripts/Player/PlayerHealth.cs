using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private PlayerRespawn respawn;

    void Start()
    {
        currentHealth = maxHealth;
        respawn = GetComponent<PlayerRespawn>();
        StartCoroutine(InitializeUI());
    }

    private IEnumerator InitializeUI()
    {
        yield return null;
        if (UIManager.instance != null)
            UIManager.instance.UpdateLives(currentHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        
        if (TryGetComponent(out PlayerAnimationController anim))
            anim.PlayHurt();

        
        if (UIManager.instance != null)
            UIManager.instance.UpdateLives(currentHealth);

        
        if (currentHealth <= 0)
            Die();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        if (UIManager.instance != null)
            UIManager.instance.UpdateLives(currentHealth);
    }

    void Die()
    {
        Debug.Log("Player morto!");
        if (respawn != null)
            respawn.Respawn();
        else
            Debug.LogWarning("PlayerRespawn non trovato sul Player!");
    }
}
