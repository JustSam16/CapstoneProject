using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DecorationPlacer_Level1 : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] groundDecorations;
    public GameObject[] airDecorations;

    [Header("Quantità")]
    public int groundCount = 40;
    public int airCount = 7;

    [Header("Range terreno (coordinate)")]
    public Vector2 groundXRange = new Vector2(-10f, 42f);
    public float castTopY = 2f;
    public float groundYOffset = 0.1f;
    public LayerMask groundLayer;

    [Header("Range cielo (coordinate)")]
    public Vector2 airXRange = new Vector2(-10f, 42f);
    public Vector2 airYRange = new Vector2(-1.2f, 0f);

    [Header("Random seed (0 = casuale ogni volta)")]
    public int seed = 0;

#if UNITY_EDITOR
    Transform EnsureChild(string name)
    {
        var t = transform.Find(name);
        if (t == null)
        {
            var go = new GameObject(name);
            go.transform.SetParent(transform);
            t = go.transform;
            t.localPosition = Vector3.zero;
        }
        return t;
    }

    void ClearChildren(Transform parent)
    {
        var list = new System.Collections.Generic.List<GameObject>();
        foreach (Transform c in parent) list.Add(c.gameObject);
        for (int i = 0; i < list.Count; i++) DestroyImmediate(list[i]);
    }

    Vector3? RaycastToGround(float x)
    {
        var origin = new Vector3(x, castTopY, 0);
        var hit = Physics2D.Raycast(origin, Vector2.down, Mathf.Infinity, groundLayer);
        if (hit.collider == null) return null;

        
        if (Mathf.Abs(hit.normal.x) > 0.3f) return null;

        return hit.point + Vector2.up * groundYOffset;
    }

    void PlaceGround(Transform parent)
    {
        if (seed != 0) Random.InitState(seed);
        for (int i = 0; i < groundCount; i++)
        {
            float x = Random.Range(groundXRange.x, groundXRange.y);
            var p = RaycastToGround(x);
            if (!p.HasValue) continue;

            var prefab = groundDecorations[Random.Range(0, groundDecorations.Length)];
            var inst = (GameObject)PrefabUtility.InstantiatePrefab(prefab, parent);
            inst.transform.position = new Vector3(p.Value.x, p.Value.y, 0);

            float s = Random.Range(0.85f, 1.15f);
            inst.transform.localScale = new Vector3(s, s, 1);
            if (Random.value < 0.5f)
                inst.transform.localScale = new Vector3(-inst.transform.localScale.x, inst.transform.localScale.y, 1);
            inst.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-2f, 2f));
        }
    }

    void PlaceAir(Transform parent)
    {
        if (seed != 0) Random.InitState(seed + 100);
        for (int i = 0; i < airCount; i++)
        {
            float x = Random.Range(airXRange.x, airXRange.y);
            float y = Random.Range(airYRange.x, airYRange.y);
            var prefab = airDecorations[Random.Range(0, airDecorations.Length)];
            var inst = (GameObject)PrefabUtility.InstantiatePrefab(prefab, parent);
            inst.transform.position = new Vector3(x, y, 0);
            float s = Random.Range(0.9f, 1.3f);
            inst.transform.localScale = new Vector3(s, s, 1);
        }
    }

    [ContextMenu("Generate All")]
    void GenerateAll()
    {
        var g = EnsureChild("Ground");
        var a = EnsureChild("Air");
        ClearChildren(g);
        ClearChildren(a);
        PlaceGround(g);
        PlaceAir(a);
        Debug.Log("Decorazioni generate (Level_1)");
    }

    [ContextMenu("Generate Ground Only")]
    void GenerateGroundOnly()
    {
        var g = EnsureChild("Ground");
        ClearChildren(g);
        PlaceGround(g);
        Debug.Log("Decorazioni terreno generate");
    }

    [ContextMenu("Generate Air Only")]
    void GenerateAirOnly()
    {
        var a = EnsureChild("Air");
        ClearChildren(a);
        PlaceAir(a);
        Debug.Log("Decorazioni cielo generate");
    }

    [ContextMenu("Clear All")]
    void ClearAll()
    {
        var g = EnsureChild("Ground");
        var a = EnsureChild("Air");
        ClearChildren(g);
        ClearChildren(a);
        Debug.Log("Decorazioni rimosse");
    }
#endif
}
