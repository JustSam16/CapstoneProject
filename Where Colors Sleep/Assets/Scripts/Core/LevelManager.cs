using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("Riferimenti")]
    public GameObject portal; 
    public UIManager uiManager; 
    public WorldColorRestorer colorRestorer; 

    private int totalFragments;
    private int currentFragments;
    private bool levelCompleted = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (uiManager == null)
            uiManager = UIManager.instance;

        totalFragments = uiManager.totalFragments;
        currentFragments = 0;

        if (portal != null)
            portal.SetActive(false); 
    }

    
    public void FragmentCollected()
    {
        currentFragments++;

        if (currentFragments >= totalFragments)
            UnlockPortal();
    }

    
    public void UnlockPortal()
    {
        if (portal != null && !portal.activeSelf)
        {
            portal.SetActive(true);

            Animator anim = portal.GetComponent<Animator>();
            if (anim != null)
                anim.speed = 1;

            Debug.Log("✅ Tutti i frammenti raccolti! Portale attivato!");
        }
    }

    
    public void LevelComplete()
    {
        if (levelCompleted) return;
        levelCompleted = true;

        Debug.Log("✨ Livello completato! Il mondo torna a colorarsi...");
        StartCoroutine(LevelEndSequence());
    }

    private IEnumerator LevelEndSequence()
    {
        
        if (colorRestorer != null)
            colorRestorer.RestoreColors();

        
        yield return new WaitForSeconds(2.5f);

        Debug.Log("Fine livello — qui puoi aggiungere la transizione o il ritorno al menu.");
    }
}
