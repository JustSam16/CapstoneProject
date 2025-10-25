using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class WorldColorRestorer : MonoBehaviour
{
    public Light2D globalLight;
    public Color startColor = new Color(0.6f, 0.6f, 0.65f, 1f);
    public Color endColor = Color.white;
    public float duration = 3f;

    public IEnumerator RestoreColors()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            globalLight.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }
    }
}
