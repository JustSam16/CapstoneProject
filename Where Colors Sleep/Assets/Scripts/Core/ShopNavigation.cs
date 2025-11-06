using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopNavigation : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ContinueToNext()
    {
        bool crouchUnlocked = PlayerPrefs.GetInt("CrouchUnlocked", 0) == 1;
        bool swimUnlocked = PlayerPrefs.GetInt("SwimUnlocked", 0) == 1;

        if (!crouchUnlocked)
            return;

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

        SceneManager.LoadScene("MainMenu");
    }
}
