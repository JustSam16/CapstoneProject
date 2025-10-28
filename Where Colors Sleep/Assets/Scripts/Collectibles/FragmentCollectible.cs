using UnityEngine;

public class FragmentCollectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (UIManager.instance != null)
            UIManager.instance.AddFragment();

        if (LevelManager.instance != null)
            LevelManager.instance.FragmentCollected();

        Destroy(gameObject);
    }
}
