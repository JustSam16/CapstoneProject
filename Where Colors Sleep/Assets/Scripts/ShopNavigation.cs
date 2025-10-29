using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopNavigation : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
