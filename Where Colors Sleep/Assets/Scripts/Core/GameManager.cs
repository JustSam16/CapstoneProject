using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int coins;
    public int totalFragments = 5;
    public int collectedFragments;

    private Vector3 lastCheckpointPosition;
    private bool hasCheckpoint;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void AddCoin()
    {
        coins++;
        Debug.Log("Monete: " + coins);
    }

    public void AddFragment()
    {
        collectedFragments++;
        Debug.Log("Frammenti: " + collectedFragments + "/" + totalFragments);

        if (collectedFragments >= totalFragments)
        {
            LevelManager.instance.UnlockPortal();
        }
    }

    public void SetCheckpoint(Vector3 pos)
    {
        lastCheckpointPosition = pos;
        hasCheckpoint = true;
        Debug.Log("Checkpoint salvato: " + pos);
    }

    public Vector3 GetCheckpointPosition()
    {
        return lastCheckpointPosition;
    }

    public bool HasCheckpoint()
    {
        return hasCheckpoint;
    }
}
