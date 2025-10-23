using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    private bool collected;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;
        if (!other.CompareTag("Player")) return;

        collected = true;

        GameManager.instance.AddCoin();
        Destroy(gameObject);
    }
}
