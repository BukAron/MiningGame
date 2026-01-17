using UnityEngine;
using TMPro;

public class Terrain : MonoBehaviour
{
    public GameObject blockPrefab;
    public GameObject specialBlockPrefab;
    [Range(0, 100)] public float specialSpawnChance = 5f;

    public int chunkSize = 32;
    public int maxHeight = 10;

    [Header("Stats")]
    public int inventoryCount = 0;      // Total blocks collected (1 per block)
    private int totalHiddenValue = 0;   // Hidden score (1 for normal, 100 for special)
    
    public TextMeshProUGUI counterText; // Shows the Inventory Count

    // This function now handles both counts
    public void AddToInventory(int pointValue)
    {
        // 1. Physical inventory always just adds 1 block
        inventoryCount += 1;

        // 2. Add the hidden value (1 or 100)
        totalHiddenValue += pointValue;

        // 3. Update UI to only show the block count
        if (counterText != null)
        {
            counterText.text = "Blocks: " + inventoryCount;
        }

        // Debug log so YOU can see the hidden value in the console
        Debug.Log("Hidden Value: " + totalHiddenValue);
    }

    public int GetValueAndClear()
    {
        int valueToSend = totalHiddenValue;
        
        // Reset inventory
        inventoryCount = 0;
        totalHiddenValue = 0;
        UpdateUI();
        
        return valueToSend; // Send the total value to the Shop
    }

    void UpdateUI()
    {
        if (counterText != null) 
            counterText.text = "Blocks: " + inventoryCount;
    }



    private void Start()
    {
        GenerateTerrain();
    }

    private void Update()
    {
        // Right Click resets everything
        if (Input.GetMouseButtonDown(1))
        {
            ResetChunk();
        }
    }

    void ResetChunk()
    {
        foreach (Transform child in transform) { Destroy(child.gameObject); }
        inventoryCount = 0;
        totalHiddenValue = 0;
        if (counterText != null) counterText.text = "Blocks: 0";
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        int halfChunksSize = chunkSize / 2;
        for(int x = -halfChunksSize; x < halfChunksSize; x++)
        {
            for(int z = -halfChunksSize; z < halfChunksSize; z++)
            {
                for(int y = 0; y > -maxHeight; y--)
                {
                    Vector3 spawnPos = new Vector3(x, y, z);
                    GameObject prefabToSpawn = (Random.Range(0f, 100f) <= specialSpawnChance) ? specialBlockPrefab : blockPrefab;

                    GameObject newBlock = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
                    newBlock.transform.parent = this.transform;
                }
            }
        }
    }
}