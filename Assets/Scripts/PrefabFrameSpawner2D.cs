using UnityEngine;
using System.Collections.Generic;

public class FrameSpawner2D : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabList;

    [Header("スポーン地点")]
    [SerializeField] private List<Transform> spawnPoints;

    void Start()
    {
        CreateRandomFrame();
    }

    void CreateRandomFrame()
    {
        if (prefabList.Count == 0) return;
        if (spawnPoints.Count == 0) return;

        // ランダムプレハブ
        GameObject prefab =
            prefabList[Random.Range(0, prefabList.Count)];

        // ランダムスポーン位置
        Transform point =
            spawnPoints[Random.Range(0, spawnPoints.Count)];

        SpriteRenderer sr =
            prefab.GetComponentInChildren<SpriteRenderer>();

        if (sr == null) return;

        // 枠生成
        GameObject frame = new GameObject("Frame");

        frame.transform.position = point.position;

        // PolygonCollider2D追加
        PolygonCollider2D poly =
            frame.AddComponent<PolygonCollider2D>();

        poly.isTrigger = true;

        // プレハブ側のPolygonColliderコピー
        PolygonCollider2D prefabPoly =
            prefab.GetComponent<PolygonCollider2D>();

        if (prefabPoly != null)
        {
            poly.points = prefabPoly.points;
        }

        // 見える枠
        LineRenderer lr = frame.AddComponent<LineRenderer>();

        lr.material =
            new Material(Shader.Find("Sprites/Default"));

        lr.useWorldSpace = false;
        lr.loop = true;
        lr.widthMultiplier = 0.05f;

        Vector2[] points = poly.points;

        lr.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i]);
        }

        // Rigidbody
        Rigidbody2D rb =
            frame.AddComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Kinematic;

        // 判定script
        FrameCollision fc =
            frame.AddComponent<FrameCollision>();

        fc.targetPrefab = new List<GameObject>() { prefab };

        fc.manager = this;
    }

    public void SpawnNext()
    {
        CreateRandomFrame();
    }
}