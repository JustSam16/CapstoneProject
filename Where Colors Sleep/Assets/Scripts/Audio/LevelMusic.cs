using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    public AudioClip levelMusic;

    void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMusic(levelMusic, true);
        }
    }
}

