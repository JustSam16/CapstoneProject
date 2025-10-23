using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public GameObject portal; 

    void Awake()
    {
        instance = this;
    }

    public void UnlockPortal()
    {
        if (portal != null)
        {
            portal.SetActive(true);
            Debug.Log("Portale attivato! Tutti i frammenti raccolti.");
        }
    }
}
