using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    public int coinValue = 1;
    public AudioClip pickupSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        int current = PlayerPrefs.GetInt("Monete", 0);
        PlayerPrefs.SetInt("Monete", current + coinValue);

        if (UIManager.instance != null)
        {
            for (int i = 0; i < coinValue; i++)
                UIManager.instance.AddCoin();
        }

        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

        Destroy(gameObject);
    }
}
