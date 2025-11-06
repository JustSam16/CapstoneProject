using UnityEngine;
using TMPro;

public class HintMessage : MonoBehaviour
{
    public TextMeshProUGUI hintText;
    public float displayTime = 3f;

    void Start()
    {
        hintText.gameObject.SetActive(true);
        Invoke(nameof(HideHint), displayTime);
    }

    void HideHint()
    {
        hintText.gameObject.SetActive(false);
    }
}

