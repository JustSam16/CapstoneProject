using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour
{
    private bool levelCompleted = false;
    public float delayBeforeShop = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelCompleted) return;

        if (collision.CompareTag("Player"))
        {
            levelCompleted = true;

            UIMessageManager ui = FindObjectOfType<UIMessageManager>();
            if (ui != null)
                ui.ShowLevelCompleteMessage();

            Debug.Log("🏁 Livello completato! Transizione in corso...");

            Invoke(nameof(LoadShop), delayBeforeShop);
        }
    }

    void LoadShop()
    {
        SceneManager.LoadScene("Shop");
    }
}

