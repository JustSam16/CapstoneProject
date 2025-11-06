using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ShopManager : MonoBehaviour
{
    public Button crouchButton;
    public Button swimButton;
    public TextMeshProUGUI coinsText;

    const int crouchCost = 5;
    const int swimCost = 5;

    int coins;
    bool crouchUnlocked;
    bool swimUnlocked;

    void OnEnable()
    {
        StartCoroutine(InitShop());
    }

    IEnumerator InitShop()
    {
        yield return new WaitForSeconds(0.1f);

        coins = PlayerPrefs.GetInt("Coins", 0);
        Debug.Log("MONETE LETTE DALLO SHOP: " + coins);

        crouchUnlocked = PlayerPrefs.GetInt("CrouchUnlocked", 0) == 1;
        swimUnlocked = PlayerPrefs.GetInt("SwimUnlocked", 0) == 1;

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
