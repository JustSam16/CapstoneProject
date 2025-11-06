using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    public int coinValue = 1;
    public AudioClip pickupSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        int current = PlayerPrefs.GetInt("Coins", 0);
        PlayerPrefs.SetInt("Coins", current + coinValue);
        PlayerPrefs.Save();

        if (UIManager.instance != null)
        {
            for (int i = 0; i < coinValue; i++)
                UIManager.instance.AddCoin();
        }

        if (AudioManager.Instance != null && pickupSound != null)
            AudioManager.Instance.PlaySFX(pickupSound);

        Destroy(gameObject);
    }
}
