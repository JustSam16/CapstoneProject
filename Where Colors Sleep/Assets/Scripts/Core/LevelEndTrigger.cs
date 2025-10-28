using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            LevelManager.instance.LevelComplete();
    }
}
