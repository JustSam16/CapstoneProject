using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class WorldColorRestorer : MonoBehaviour
{
    [Header("Luce globale")]
    public Light2D globalLight;

    [Header("Colori")]
    public Color startColor = new Color(0.6f, 0.6f, 0.65f, 1f); 
    public Color endColor = Color.white; 
    public float duration = 3f; 

    private bool isRestoring = false;

    void Start()
    {
        if (globalLight != null)
            globalLight.color = startColor; 
    }

    
    public void RestoreColors()
    {
        if (!isRestoring && globalLight != null)
            StartCoroutine(RestoreSequence());
    }

    private IEnumerator RestoreSequence()
    {
        isRestoring = true;

        float t = 0f;
        Color initial = globalLight.color;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            globalLight.color = Color.Lerp(initial, endColor, t);
            yield return null;
        }

        globalLight.color = endColor;
        isRestoring = false;

        Debug.Log("🌈 Il mondo ha riacquistato i suoi colori!");
    }
}
