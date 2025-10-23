using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public Transform cameraTransform;
    public float parallaxFactor = 0.5f;

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = cameraTransform.position.x * parallaxFactor;
        transform.position = pos;
    }
}
