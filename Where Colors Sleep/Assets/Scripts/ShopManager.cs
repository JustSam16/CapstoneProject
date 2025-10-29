using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI crystalsText;
    public Button crouchButton;
    public Button swimButton;

    int crystals;
    int crouchCost = 25;
    int swimCost = 40;

    void Start()
    {
        crystals = PlayerPrefs.GetInt("Crystals", 0);
        crystalsText.text = "Crystals: " + crystals.ToString();

        if (PlayerPrefs.GetInt("CrouchUnlocked", 0) == 1)
            SetButtonBought(crouchButton);

        if (PlayerPrefs.GetInt("SwimUnlocked", 0) == 1)
            SetButtonBought(swimButton);
    }

    public void BuyCrouch()
    {
        if (crystals >= crouchCost && PlayerPrefs.GetInt("CrouchUnlocked", 0) == 0)
        {
            crystals -= crouchCost;
            PlayerPrefs.SetInt("Crystals", crystals);
            PlayerPrefs.SetInt("CrouchUnlocked", 1);
            SetButtonBought(crouchButton);
            crystalsText.text = "Crystals: " + crystals.ToString();
        }
    }

    public void BuySwim()
    {
        if (crystals >= swimCost && PlayerPrefs.GetInt("SwimUnlocked", 0) == 0)
        {
            crystals -= swimCost;
            PlayerPrefs.SetInt("Crystals", crystals);
            PlayerPrefs.SetInt("SwimUnlocked", 1);
            SetButtonBought(swimButton);
            crystalsText.text = "Crystals: " + crystals.ToString();
        }
    }

    void SetButtonBought(Button btn)
    {
        btn.interactable = false;
        btn.GetComponentInChildren<TextMeshProUGUI>().text = "Purchased";
    }
}
