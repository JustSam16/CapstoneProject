using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ShopManager : MonoBehaviour
{
    public Button crouchButton;
    public Button swimButton;
    public TextMeshProUGUI coinsText;

    public int crouchCost = 5;
    public int swimCost = 5;

    int coins;
    bool crouchUnlocked;
    bool swimUnlocked;

    void Start()
    {
        StartCoroutine(ForceEverything());
    }

    IEnumerator ForceEverything()
    {
        
        yield return new WaitForSeconds(0.5f);

        coins = PlayerPrefs.GetInt("Coins", 0);
        crouchUnlocked = PlayerPrefs.GetInt("CrouchUnlocked", 0) == 1;
        swimUnlocked = PlayerPrefs.GetInt("SwimUnlocked", 0) == 1;

        RefreshUI();

        
        yield return new WaitForSeconds(2f);
        RefreshUI();
    }

    public void BuyCrouch()
    {
        if (crouchUnlocked) return;
        if (coins < crouchCost) return;

        coins -= crouchCost;
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetInt("CrouchUnlocked", 1);
        PlayerPrefs.Save();
        crouchUnlocked = true;
        RefreshUI();
    }

    public void BuySwim()
    {
        if (swimUnlocked) return;
        if (coins < swimCost) return;

        coins -= swimCost;
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetInt("SwimUnlocked", 1);
        PlayerPrefs.Save();
        swimUnlocked = true;
        RefreshUI();
    }

    void RefreshUI()
    {
        if (coinsText != null)
            coinsText.text = "Monete: " + coins;

        UpdateButton(crouchButton, crouchUnlocked, crouchCost);
        UpdateButton(swimButton, swimUnlocked, swimCost);
    }

    void UpdateButton(Button button, bool unlocked, int cost)
    {
        if (button == null) return;
        var txt = button.GetComponentInChildren<TextMeshProUGUI>();
        if (txt == null) return;

        if (unlocked)
        {
            button.interactable = false;
            txt.text = "Comprato";
        }
        else
        {
            button.interactable = true;
            txt.text = "Comprare (" + cost + " monete)";
        }
    }
}
