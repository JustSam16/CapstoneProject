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
        string nextScene = "";

        
        if (PlayerPrefs.GetInt("CrouchUnlocked", 0) == 0)
        {
            
            nextScene = "Level2_Cave";
        }
        else if (PlayerPrefs.GetInt("CrouchUnlocked", 0) == 1 && PlayerPrefs.GetInt("SwimUnlocked", 0) == 0)
        {
            
            nextScene = "Level3_UnderWater";
        }
        else
        {
            
            nextScene = "MainMenu";
        }

        if (!string.IsNullOrEmpty(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.LogWarning("Nessuna scena trovata per il prossimo step!");
        }
    }
}
