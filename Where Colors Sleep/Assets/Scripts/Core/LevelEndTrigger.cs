using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    private bool levelCompleted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelCompleted) return;

        if (collision.CompareTag("Player"))
        {
            levelCompleted = true;

            
            UIMessageManager ui = FindObjectOfType<UIMessageManager>();
            if (ui != null)
                ui.ShowLevelCompleteMessage();

            Debug.Log("🏁 Livello completato!");
        }
    }
}
