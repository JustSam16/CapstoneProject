using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Vita (cuori)")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    [Header("Frammenti e Monete")]
    public TMP_Text fragmentsText;
    public TMP_Text coinsText;

    private int fragments;
    private int totalFragments = 5;
    private int coins;

    void Awake()
    {
        instance = this;
    }

    
    public void UpdateLives(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (currentHealth >= (i + 1) * 2)
                hearts[i].sprite = fullHeart;
            else if (currentHealth == (i * 2) + 1)
                hearts[i].sprite = halfHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }

    
    public void AddFragment()
    {
        fragments++;
        if (fragmentsText != null)
            fragmentsText.text = fragments + " / " + totalFragments;
    }

    
    public void AddCoin()
    {
        coins++;
        if (coinsText != null)
            coinsText.text = "x " + coins;
    }
}
