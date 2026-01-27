using UnityEngine;
using TMPro;
using System.Collections;

[System.Serializable]
public class BlockTier
{
    public string name;
    public int upgradeCost;
    public GameObject normalPrefab;
    public GameObject specialPrefab;
    public int normalValue;
    public int normalHealth;
    public int specialValue;
    public int specialHealth;
}

public class Terrain : MonoBehaviour
{
    [Header("Block Tiers")]
    public BlockTier[] tiers; 
    public int currentTerrainLevel = 0; 

    [Header("Settings")]
    [Range(0, 100)] public float specialSpawnChance = 5f;
    public int chunkSize = 32;
    public int maxHeight = 10;

    [Header("Stats")]
    public int inventoryCount = 0;
    private int totalHiddenValue = 0;
    public TextMeshProUGUI counterText;

    [Header("Depth Settings")]
    public float baseSpecialChance = 5f;
    public float chanceIncreasePerLayer = 2f;
    public float maxSpecialChance = 50f;
    
    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

        if (LoadingManager.Instance != null)
        {
            StartCoroutine(GenerateTerrain());
        }
    } 

// upgrade terrain level
    public void UpgradeTerrainLevel()
    {
        if (currentTerrainLevel < tiers.Length - 1)
        {
            currentTerrainLevel++;
            ResetChunk(); 
        }
    }
// simple inv counter
    public void AddToInventory(int pointValue)
    {
        inventoryCount += 1;
        totalHiddenValue += pointValue;
        UpdateUI();
    }
// get inv value and clear when needed
    public int GetValueAndClear()
    {
        int valueToSend = totalHiddenValue;
        inventoryCount = 0;
        totalHiddenValue = 0;
        UpdateUI();
        return valueToSend;
    }
// update inv ui
    void UpdateUI()
    {
        if (counterText != null) 
            counterText.text = "Blocks: " + inventoryCount;
    }

// the main reset chunk function
    public void ResetChunk()
    {
        StopAllCoroutines();
        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        System.GC.Collect();
        StartCoroutine(GenerateTerrain());
    }


// changed to IEnumerator for optimization
        IEnumerator GenerateTerrain()
        
    {
        int halfChunksSize = chunkSize / 2;
        int blocksSpawnedThisFrame = 0;
        int spawnLimit = 250;

        float totalBlocks = chunkSize * chunkSize * maxHeight;
        float currentBlocks = 0;

        yield return StartCoroutine(LoadingManager.Instance.LoadScene());

        for(int x = -halfChunksSize; x < halfChunksSize; x++)
        {
            for(int z = -halfChunksSize; z < halfChunksSize; z++)
            {
                for(int y = 0; y > -maxHeight; y--)
                {
                    Vector3 spawnPos = new Vector3(x, y, z);
                    
                    int randomTierIndex = Random.Range(0, currentTerrainLevel + 1);
                    BlockTier selectedTier = tiers[randomTierIndex];

                    float currentDepthChance = baseSpecialChance + (Mathf.Abs(y) * chanceIncreasePerLayer);
                    currentDepthChance = Mathf.Min(currentDepthChance, maxSpecialChance);

                    bool isSpecial = Random.Range(0f, 100f) <= currentDepthChance;
                    GameObject prefabToSpawn = isSpecial ? selectedTier.specialPrefab : selectedTier.normalPrefab;

                    if (prefabToSpawn != null)
                    {
                        GameObject newBlock = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
                        newBlock.transform.parent = this.transform;

                        Block blockScript = newBlock.GetComponent<Block>();
                        if (blockScript != null)
                        {
                            int healthToSet = isSpecial ? selectedTier.specialHealth : selectedTier.normalHealth;
                            int valueToSet = isSpecial ? selectedTier.specialValue : selectedTier.normalValue;
                            blockScript.SetupBlock(healthToSet, valueToSet);
                        }
                    }

                    currentBlocks++;
                    LoadingManager.Instance.UpdateProgress(currentBlocks / totalBlocks);

                    blocksSpawnedThisFrame++;
                    if (blocksSpawnedThisFrame >= spawnLimit)
                    {
                        blocksSpawnedThisFrame = 0;
                        yield return null;
                    }
                }
            }
        }
        LoadingManager.Instance.UnloadLoadingScreen();
    }
}