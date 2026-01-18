using UnityEngine;

public class PickaxeShop : MonoBehaviour
{
    public static bool IsAnyShopOpen = false;

    [Header("Data")]
    public PickaxeLevel[] upgrades;
    public ShopSlot[] uiSlots;
    
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

        PlayerStats stats = FindFirstObjectByType<PlayerStats>();
        

        int count = Mathf.Min(uiSlots.Length, upgrades.Length);
        
        for (int i = 0; i < count; i++)
        {
            uiSlots[i].Setup(upgrades[i], stats);
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
    }

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
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
        {
            return hit.collider.gameObject == this.gameObject;
        }
        return false;
    }
}