using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectManager : MonoBehaviour
{
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public TextMeshProUGUI req2Text;
    public TextMeshProUGUI req3Text;

    void Start()
    {
        level1Button.interactable = true;

        bool crouch = PlayerPrefs.GetInt("CrouchUnlocked", 0) == 1;
        bool swim = PlayerPrefs.GetInt("SwimUnlocked", 0) == 1;

        level2Button.interactable = crouch;
        level3Button.interactable = swim;

        if (req2Text != null) req2Text.gameObject.SetActive(!crouch);
        if (req3Text != null) req3Text.gameObject.SetActive(!swim);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2()
    {
        if (PlayerPrefs.GetInt("CrouchUnlocked", 0) == 1)
            SceneManager.LoadScene("Level2");
    }

    public void LoadLevel3()
    {
        if (PlayerPrefs.GetInt("SwimUnlocked", 0) == 1)
            SceneManager.LoadScene("Level3");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
