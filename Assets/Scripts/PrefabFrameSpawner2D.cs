using UnityEngine;
using System.Collections.Generic;

public class FrameSpawner2D : MonoBehaviour
{
    [Header("生成するプレハブ")]
    [SerializeField] private List<GameObject> prefabList;

    [Header("スポーン地点")]
    [SerializeField] private List<Transform> spawnPoints;

    [Header("枠の線の太さ")]
    [SerializeField] private float lineWidth = 0.05f;

    [Header("スポーン位置チェック半径")]
    [SerializeField] private float checkRadius = 0.5f;

    void Start()
    {
        CreateRandomFrame();
    }

    void CreateRandomFrame()
    {
        // プレハブ未設定
        if (prefabList == null || prefabList.Count == 0)
        {
            Debug.LogError("PrefabList が空");
            return;
        }

        // スポーン地点未設定
        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogError("SpawnPoints が空");
            return;
        }

        // 空いている地点を探す
        List<Transform> freePoints = new List<Transform>();

        foreach (Transform point in spawnPoints)
        {
            Collider2D[] hits =
                Physics2D.OverlapCircleAll(
                    point.position,
                    checkRadius
                );

            bool occupied = false;

            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Frame"))
                {
                    occupied = true;
                    break;
                }
            }

            if (!occupied)
            {
                freePoints.Add(point);
            }
        }

        // 空き無し
        if (freePoints.Count == 0)
        {
            Debug.Log("空いてるスポーン地点なし");
            return;
        }

        // ランダムプレハブ
        GameObject prefab =
            prefabList[Random.Range(0, prefabList.Count)];

        if (prefab == null)
        {
            Debug.LogError("Prefab が NULL");
            return;
        }

        // ランダム空き地点
        Transform spawnPoint =
            freePoints[Random.Range(0, freePoints.Count)];

        // SpriteRenderer確認
        SpriteRenderer sr =
            prefab.GetComponentInChildren<SpriteRenderer>();

        if (sr == null)
        {
            Debug.LogError(prefab.name + " に SpriteRenderer がない");
            return;
        }

        // 枠生成
        GameObject frame = new GameObject("Frame");

        frame.tag = "Frame";

        frame.transform.position = spawnPoint.position;

        // PolygonCollider2D
        PolygonCollider2D poly =
            frame.AddComponent<PolygonCollider2D>();

        poly.isTrigger = true;

        // プレハブ側Collider取得
        PolygonCollider2D prefabPoly =
            prefab.GetComponent<PolygonCollider2D>();

        if (prefabPoly != null)
        {
            poly.points = prefabPoly.points;
        }
        else
        {
            Debug.LogError(
                prefab.name +
                " に PolygonCollider2D がありません"
            );

            Destroy(frame);

            return;
        }

        // 線描画
        LineRenderer lr =
            frame.AddComponent<LineRenderer>();

        lr.material =
            new Material(Shader.Find("Sprites/Default"));

        lr.useWorldSpace = false;

        lr.loop = true;

        lr.widthMultiplier = lineWidth;

        Vector2[] points = poly.points;

        lr.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i]);
        }

        // Rigidbody2D
        Rigidbody2D rb =
            frame.AddComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Kinematic;

        // 判定script
        FrameCollision fc =
            frame.AddComponent<FrameCollision>();

        fc.targetPrefab =
            new List<GameObject>() { prefab };

        fc.manager = this;
    }

    // 次生成
    public void SpawnNext()
    {
        CreateRandomFrame();
    }

    // Sceneでスポーン範囲確認
    private void OnDrawGizmosSelected()
    {
        if (spawnPoints == null) return;

        Gizmos.color = Color.green;

        foreach (Transform point in spawnPoints)
        {
            if (point == null) continue;

            Gizmos.DrawWireSphere(
                point.position,
                checkRadius
            );
        }
    }
}