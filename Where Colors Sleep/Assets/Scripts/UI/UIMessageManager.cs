using UnityEngine;
using TMPro;
using System.Collections;

public class UIMessageManager : MonoBehaviour
{
    public TMP_Text checkpointText;
    public TMP_Text levelCompleteText;
    public float displayDuration = 2f;

    public void ShowCheckpointMessage()
    {
        StartCoroutine(ShowMessage(checkpointText));
    }

    public void ShowLevelCompleteMessage()
    {
        StartCoroutine(ShowMessage(levelCompleteText));
    }

    IEnumerator ShowMessage(TMP_Text text)
    {
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(displayDuration);
        text.gameObject.SetActive(false);
    }
}
