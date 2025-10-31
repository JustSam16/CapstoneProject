using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectManager : MonoBehaviour
{
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public TextMeshProUGUI req2Text;
    public TextMeshProUGUI req3Text;
    public Image lock2Icon;
    public Image lock3Icon;

    public Sprite closedLockSprite;
    public Sprite openLockSprite;

    private Color unlockedColor = new Color(0.75f, 0.6f, 0.9f);
    private Color lockedColor = new Color(0.6f, 0.6f, 0.6f);

    void Start()
    {
        
        level1Button.interactable = true;
        level2Button.interactable = true;
        level3Button.interactable = true;

        UpdateButtonVisual(level2Button, req2Text, lock2Icon, true, "Crouch");
        UpdateButtonVisual(level3Button, req3Text, lock3Icon, true, "Swim");
    }

    void UpdateButtonVisual(Button button, TextMeshProUGUI reqText, Image lockIcon, bool unlocked, string abilityName)
    {
        if (button == null) return;

        TextMeshProUGUI btnText = button.GetComponentInChildren<TextMeshProUGUI>();

        button.image.color = unlockedColor;
        if (btnText != null) btnText.text = "SBLOCCATO";
        if (reqText != null) reqText.gameObject.SetActive(false);

        if (lockIcon != null && openLockSprite != null)
            lockIcon.sprite = openLockSprite;
    }

    public void LoadLevel1() => SceneManager.LoadScene("SampleScene");
    public void LoadLevel2() => SceneManager.LoadScene("Level2_Cave");
    public void LoadLevel3() => SceneManager.LoadScene("Level3_UnderWater");
    public void BackToMenu() => SceneManager.LoadScene("MainMenu");
}
