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

    private void Update()
    {
        // 0 is Left Click, 1 is Right Click, 2 is Middle Click
        if (Input.GetMouseButtonDown(1)) 
        {
            ResetChunk();
        }
        
        // OR: If you meant the "Right Arrow" keyboard key, use this instead:
        // if (Input.GetKeyDown(KeyCode.RightArrow))
        // {
        //     ResetChunk();
        // }
    }

    void ResetChunk()
    {
        // 1. Destroy all existing blocks that are children of this object
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // 2. Generate new terrain
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
