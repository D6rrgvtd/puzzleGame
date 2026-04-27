using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public int width = 4;
    public int height = 4;
    public GameObject tilePrefab;

    private GameObject[,] tiles;

    void Start()
    {
        tiles = new GameObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 pos = new Vector2(x, y);
                GameObject tile = Instantiate(tilePrefab, pos, Quaternion.identity);
                tiles[x, y] = tile;
            }
        }
    }
}