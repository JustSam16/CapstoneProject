using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopNavigation : MonoBehaviour
{
    [Header("Scena successiva dopo gli acquisti")]
    public string nextSceneName = "TestAbilities";

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ContinueToNext()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Nessuna scena impostata come 'nextSceneName'!");
        }
    }
}
