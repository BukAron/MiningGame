using UnityEngine;

public class BlockShop : MonoBehaviour
{
    [Header("References")]
    public Terrain terrainScript;
    public PlayerStats playerStats;

    [Header("UI Slots")]
    public BlockSlot[] uiSlots;
    
    [Header("Settings")]
    public float interactionRange = 5f;
    public Transform playerTransform;
    
    [Header("UI Elements")]
    public GameObject interactPrompt;
    public GameObject shopPanel;
    public Camera playerCam;

    [HideInInspector] public bool isShopOpen = false;

    private void Start()
    {
        if (playerStats == null) playerStats = FindFirstObjectByType<PlayerStats>();

        for (int i = 0; i < uiSlots.Length; i++)
        {
            int tierIndex = i + 1; 

            if (tierIndex < terrainScript.tiers.Length)
            {
                uiSlots[i].gameObject.SetActive(true);
                uiSlots[i].Setup(terrainScript.tiers[tierIndex], playerStats);
            }
            else
            {

                uiSlots[i].gameObject.SetActive(false);
            }
        }

        if (shopPanel != null) shopPanel.SetActive(false);
    }

    void Update()
    {
        if (isShopOpen)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
            {
                ToggleShop(false);
            }
            return;
        }


        if (PickaxeShop.IsAnyShopOpen) return;

        bool lookingAtShop = IsPlayerLookingAtShop();
        interactPrompt.SetActive(lookingAtShop);

        if (lookingAtShop && Input.GetKeyDown(KeyCode.E))
        {
            ToggleShop(true);
        }
    }

    public void ToggleShop(bool state)
    {
        isShopOpen = state;
        shopPanel.SetActive(state);

        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = state;
        
        PickaxeShop.IsAnyShopOpen = state; 
    }

    public void BuyUpgrade(int slotIndex)
    {

        int tierIndex = slotIndex + 1;

        if (tierIndex < terrainScript.tiers.Length && playerStats != null)
        {
            BlockTier tierToBuy = terrainScript.tiers[tierIndex];


            if (tierIndex <= terrainScript.currentTerrainLevel)
            {
                Debug.Log("Already unlocked!");
                return;
            }


            if (playerStats.money >= tierToBuy.upgradeCost)
            {
                playerStats.money -= tierToBuy.upgradeCost;
                

                terrainScript.currentTerrainLevel = tierIndex;
                terrainScript.ResetChunk();

                Debug.Log("Purchased: " + tierToBuy.name);
            }
            else
            {
                Debug.Log("Not enough money!");
            }
        }
    }

    bool IsPlayerLookingAtShop()
    {
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
        {
            return hit.collider.gameObject == this.gameObject;
        }
        return false;
    }
}