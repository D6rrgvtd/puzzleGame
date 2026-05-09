using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TileSpawner : MonoBehaviour
{
    public List<GameObject> puzzlePrefab;
  
    public List<Transform> SpawnPoints;
    void Start()
    {
        SpawnAllEmptySlots();
    }

    public void SpawnAllEmptySlots()
    {
        if (puzzlePrefab == null || puzzlePrefab.Count == 0) return;

        foreach (Transform point in SpawnPoints)
        {
           
            if (point.childCount == 0)
            {
                GameObject selectedPrefab = puzzlePrefab[Random.Range(0, puzzlePrefab.Count)];
                GameObject newTile = Instantiate(selectedPrefab, point);
                newTile.transform.localPosition = Vector3.zero;
            }
        }
    }

}

