using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public GameObject level2LockIcon;
    public GameObject level3LockIcon;

    void OnEnable()
    {
        Refresh();
    }

    void Refresh()
    {
        bool crouchUnlocked = PlayerPrefs.GetInt("CrouchUnlocked", 0) == 1;
        bool swimUnlocked = PlayerPrefs.GetInt("SwimUnlocked", 0) == 1;

        if (level1Button != null) level1Button.interactable = true;
        if (level2Button != null) level2Button.interactable = crouchUnlocked;
        if (level3Button != null) level3Button.interactable = crouchUnlocked && swimUnlocked;

        if (level2LockIcon != null) level2LockIcon.SetActive(!crouchUnlocked);
        if (level3LockIcon != null) level3LockIcon.SetActive(!(crouchUnlocked && swimUnlocked));
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadLevel2()
    {
        if (PlayerPrefs.GetInt("CrouchUnlocked", 0) == 1)
            SceneManager.LoadScene("Level2_Cave");
    }

    public void LoadLevel3()
    {
        if (PlayerPrefs.GetInt("CrouchUnlocked", 0) == 1 && PlayerPrefs.GetInt("SwimUnlocked", 0) == 1)
            SceneManager.LoadScene("Level3_UnderWater");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
