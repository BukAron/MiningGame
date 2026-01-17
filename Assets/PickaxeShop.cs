using UnityEngine;

public class PickaxeShop : MonoBehaviour
{
    public PickaxeLevel[] upgrades; // Add your pickaxes here in Inspector
    int currentUpgradeIndex = 0;

    public float interactionRange = 5f;
    public Transform playerTransform;
    public GameObject interactUI;

    void Update()
    {
        // Reuse your "Looking at Shop" logic here
        if (IsPlayerLookingAtShop())
        {
            interactUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                BuyNextPickaxe();
            }
        }
        else
        {
            interactUI.SetActive(false);
        }
    }

    void BuyNextPickaxe()
    {
        if (currentUpgradeIndex >= upgrades.Length)
        {
            Debug.Log("Already at Max Level!");
            return;
        }

        PlayerStats stats = playerTransform.GetComponent<PlayerStats>();
        PickaxeLevel nextLevel = upgrades[currentUpgradeIndex];

        if (stats.TryBuyUpgrade(nextLevel))
        {
            currentUpgradeIndex++; // Move to the next available upgrade
        }
    }

    bool IsPlayerLookingAtShop()
    {
        // Standard raycast check (same as your other shop)
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
        {
            return hit.collider.gameObject == this.gameObject;
        }
        return false;
    }
}
