using System.Collections.Generic;
using UnityEngine;

public class FrameCollision : MonoBehaviour
{
    public List<GameObject> targetPrefab;

    public FrameSpawner2D manager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        string otherName =
            other.gameObject.name.Replace("(Clone)", "");

        foreach (GameObject prefab in targetPrefab)
        {
            if (prefab == null) continue;

            if (otherName == prefab.name)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);

                manager.SpawnNext();

                return;
            }
        }
    }
}