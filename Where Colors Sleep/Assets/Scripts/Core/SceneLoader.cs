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

    public void LoadNextScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        switch (currentScene)
        {
            case "SampleScene":
                SceneManager.LoadScene("Level2_Cave");
                break;

            case "Level2_Cave":
                SceneManager.LoadScene("Level3_UnderWater");
                break;

            case "Level3_UnderWater":
                SceneManager.LoadScene("MainMenu");
                break;

            default:
                SceneManager.LoadScene("MainMenu");
                break;
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
