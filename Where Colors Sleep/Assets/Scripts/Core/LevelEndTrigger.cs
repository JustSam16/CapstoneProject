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

            if (AudioManager.Instance != null)
                AudioManager.Instance.StopMusic();

            UIMessageManager ui = FindObjectOfType<UIMessageManager>();
            if (ui != null)
                ui.ShowLevelCompleteMessage();

            Invoke(nameof(LoadNextScene), delayBeforeNextScene);
        }
    }

    void LoadNextScene()
    {
        PlayerPrefs.Save(); 

        SceneLoader loader = FindObjectOfType<SceneLoader>();
        if (loader != null)
        {
            loader.LoadNextScene();
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Shop");
        }
    }
}
