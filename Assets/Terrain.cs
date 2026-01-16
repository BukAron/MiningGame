using UnityEngine;

public class Terrain : MonoBehaviour
{
    public GameObject blockPrefab;

    public int chunkSize = 32;
    public int maxHeight = 10;


    private void Start()
    {
        GenerateTerrain();
    }


    void GenerateTerrain()
    {
       int halfChunksSize  = chunkSize / 2;

       for(int x = -halfChunksSize; x < halfChunksSize; x++)
       {
           for(int z = -halfChunksSize; z < halfChunksSize; z++)
            {
                int totalHeight = maxHeight; // You can modify this to create varied terrain heights
                for(int y = 0; y > -totalHeight; y--)
                {
                Vector3 spawnPos = new Vector3(x, y, z);

                Instantiate(blockPrefab, spawnPos, Quaternion.identity);
                }
            }

        }
    }
}
