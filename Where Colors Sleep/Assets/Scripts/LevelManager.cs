using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("Riferimenti")]
    public GameObject portal;                 
    public WorldColorRestorer colorRestorer;  

    private bool levelCompleted;

    void Awake()
    {
        instance = this;
    }

    
    public void UnlockPortal()
    {
        if (portal != null && !portal.activeSelf)
        {
            portal.SetActive(true);

            
            Animator anim = portal.GetComponent<Animator>();
            if (anim != null)
                anim.speed = 1;

            Debug.Log("Portale attivato! Tutti i frammenti raccolti.");
        }
    }

    
    public void LevelComplete()
    {
        if (levelCompleted) return;
        levelCompleted = true;

        Debug.Log("Livello completato!");
        StartCoroutine(FinishSequence());
    }

    private IEnumerator FinishSequence()
    {
        
        if (colorRestorer != null)
            yield return StartCoroutine(colorRestorer.RestoreColors());

        Debug.Log("Colori tornati!");
    }
}
