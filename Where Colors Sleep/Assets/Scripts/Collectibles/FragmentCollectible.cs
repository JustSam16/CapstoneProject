using UnityEngine;

public class FragmentCollectible : MonoBehaviour
{
    public AudioClip pickupSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (AudioManager.Instance != null && pickupSound != null)
            AudioManager.Instance.PlaySFX(pickupSound);

        if (UIManager.instance != null)
            UIManager.instance.AddFragment();

        if (LevelManager.instance != null)
            LevelManager.instance.FragmentCollected();

        Destroy(gameObject);
    }
}
