using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    private bool levelCompleted = false;
    public float delayBeforeNextScene = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelCompleted) return;

        if (collision.CompareTag("Player"))
        {
            levelCompleted = true;

            UIMessageManager ui = FindObjectOfType<UIMessageManager>();
            if (ui != null)
                ui.ShowLevelCompleteMessage();

            Debug.Log("Livello completato! Transizione in corso...");

            Invoke(nameof(LoadNextScene), delayBeforeNextScene);
        }
    }

    void LoadNextScene()
    {
        SceneLoader loader = FindObjectOfType<SceneLoader>();
        if (loader != null)
        {
            loader.LoadNextScene();
        }
        else
        {
            Debug.LogWarning("Nessun SceneLoader trovato nella scena, caricamento manuale Shop...");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Shop");
        }
    }
}
