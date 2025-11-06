using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, IPointerClickHandler
{
    public AudioClip clickSound;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (AudioManager.Instance != null && clickSound != null)
            AudioManager.Instance.PlaySFX(clickSound);
    }
}
