using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void LoadShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void LoadNextScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        switch (currentScene)
        {
            case "SampleScene":
                SceneManager.LoadScene("Shop");
                break;

            case "Level2_Cave":
                SceneManager.LoadScene("Shop");
                break;

            case "Level3_UnderWater":
                SceneManager.LoadScene("VictoryMenu");
                break;

            case "Shop":
                HandleShopProgression();
                break;

            default:
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }

    void HandleShopProgression()
    {
        bool crouchUnlocked = PlayerPrefs.GetInt("CrouchUnlocked", 0) == 1;
        bool swimUnlocked = PlayerPrefs.GetInt("SwimUnlocked", 0) == 1;

        if (crouchUnlocked && !swimUnlocked)
        {
            SceneManager.LoadScene("Level2_Cave");
            return;
        }

        if (crouchUnlocked && swimUnlocked)
        {
            SceneManager.LoadScene("Level3_UnderWater");
            return;
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
