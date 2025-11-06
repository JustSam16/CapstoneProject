using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button restartButton;
    public Button mainMenuButton;
    public Button shopButton;
    public Button audioButton;
    public Sprite audioOnSprite;
    public Sprite audioOffSprite;

    private bool isPaused = false;
    private bool isAudioOn = true;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        restartButton.onClick.AddListener(RestartLevel);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        shopButton.onClick.AddListener(GoToShop);
        audioButton.onClick.AddListener(ToggleAudio);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                Pause();
            else
                Resume();
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    void GoToShop()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Shop");
    }

    void ToggleAudio()
    {
        isAudioOn = !isAudioOn;
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetMasterMute(!isAudioOn);

        Image icon = audioButton.GetComponent<Image>();
        if (icon != null)
            icon.sprite = isAudioOn ? audioOnSprite : audioOffSprite;
    }
}
