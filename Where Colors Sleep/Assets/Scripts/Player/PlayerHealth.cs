using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Impostazioni Vita")]
    public int maxHealth = 6; 
    public int currentHealth;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    private PlayerRespawn respawn;

    void Start()
    {
        currentHealth = maxHealth;
        respawn = GetComponent<PlayerRespawn>();
        UpdateHealthUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        Debug.Log($"💔 Danno preso! Vita attuale: {currentHealth}");
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            StartCoroutine(RespawnCoroutine());
        }
    }

    System.Collections.IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        if (respawn != null)
            respawn.RespawnPlayer();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            int heartHealth = Mathf.Clamp(currentHealth - (i * 2), 0, 2);

            if (heartHealth == 2)
                hearts[i].sprite = fullHeart;
            else if (heartHealth == 1)
                hearts[i].sprite = halfHeart;
            else
                hearts[i].sprite = emptyHeart;

            hearts[i].enabled = i < Mathf.CeilToInt(maxHealth / 2f);
        }
    }
}
