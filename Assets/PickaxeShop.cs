using UnityEngine;

public class PickaxeShop : MonoBehaviour
{
    [Header("Data")]
    public PickaxeLevel[] upgrades; // Add your pickaxes here in Inspector
    public ShopSlot[] uiSlots;      // Drag your Item(2) objects here
    
    [Header("Settings")]
    public float interactionRange = 5f;
    public Transform playerTransform;
    
    [Header("UI Elements")]
    public GameObject interactPrompt; // "Press E to Open" text
    public GameObject shopPanel;      // Drag 'PICKAXE SHOP UI' here
    public Camera playerCam;          // Assign your Main Camera

    private bool isShopOpen = false;

    private void Start()
    {
        // Link the UI slots to the data on start
        PlayerStats stats = FindFirstObjectByType<PlayerStats>();
        
        // Ensure we don't crash if arrays are different sizes
        int count = Mathf.Min(uiSlots.Length, upgrades.Length);
        
        for (int i = 0; i < count; i++)
        {
            uiSlots[i].Setup(upgrades[i], stats);
        }

        // Make sure shop starts closed
        if (shopPanel != null) shopPanel.SetActive(false);
    }

    void Update()
    {
        // If shop is open, check for Close input
        if (isShopOpen)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
            {
                ToggleShop(false);
            }
            return; // Don't look for the shop while menu is open
        }

        // Logic for looking at the shop
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

        // This allows you to use the mouse in the menu
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = state;
    }

    // This is called by your UI Buttons (On Click)
    public void BuyUpgrade(int index)
    {
        PlayerStats stats = playerTransform.GetComponent<PlayerStats>();
        
        if (index < upgrades.Length && stats != null)
        {
            if (stats.TryBuyUpgrade(upgrades[index]))
            {
                Debug.Log("Purchased: " + upgrades[index].name);
            }
        }
    }

    bool IsPlayerLookingAtShop()
    {
        // Using playerCam variable is more reliable than Camera.main
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
        {
            return hit.collider.gameObject == this.gameObject;
        }
        return false;
    }
}