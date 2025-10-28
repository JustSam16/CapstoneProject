using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (UIManager.instance != null)
            UIManager.instance.AddCoin();

        Destroy(gameObject);
    }
}
