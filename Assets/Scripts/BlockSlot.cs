using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlockSlot : MonoBehaviour
{
    [Header("References")]
    public Image buyButtonImage;
    public TextMeshProUGUI priceText; 
    public TextMeshProUGUI nameText;

    private BlockTier myData;
    private PlayerStats playerStats;

    public void Setup(BlockTier data, PlayerStats stats)
    {
        myData = data;
        playerStats = stats;
        if (nameText != null) nameText.text = data.name;
        
        if (priceText != null) priceText.text = "$" + data.upgradeCost;
    }

    void Update()
    {
        if (playerStats == null || myData == null) return;

        
        
    }
}