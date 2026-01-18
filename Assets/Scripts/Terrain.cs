using UnityEngine;
using TMPro;

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

    public void UpgradeTerrainLevel()
    {
        if (currentTerrainLevel < tiers.Length - 1)
        {
            currentTerrainLevel++;
            ResetChunk(); 
        }
    }

    public void AddToInventory(int pointValue)
    {
        inventoryCount += 1;
        totalHiddenValue += pointValue;
        UpdateUI();
    }

    public int GetValueAndClear()
    {
        int valueToSend = totalHiddenValue;
        inventoryCount = 0;
        totalHiddenValue = 0;
        UpdateUI();
        return valueToSend;
    }

    void UpdateUI()
    {
        if (counterText != null) 
            counterText.text = "Blocks: " + inventoryCount;
    }

    private void Start() => GenerateTerrain();

    public void ResetChunk()
    {
        foreach (Transform child in transform) { Destroy(child.gameObject); }
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
                    
                    int randomTierIndex = Random.Range(0, currentTerrainLevel + 1);
                    BlockTier selectedTier = tiers[randomTierIndex];

                    bool isSpecial = Random.Range(0f, 100f) <= specialSpawnChance;
                    GameObject prefabToSpawn = isSpecial ? selectedTier.specialPrefab : selectedTier.normalPrefab;

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
            }
        }
    }
}