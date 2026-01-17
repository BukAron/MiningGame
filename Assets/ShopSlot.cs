using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    [Header("References")]
    public Image buyButtonImage;
    public TextMeshProUGUI priceText;
    public Image pickaxeIcon; // The grey part for the image

    [Header("Colors")]
    public Color affordableColor = Color.green;
    public Color expensiveColor = Color.red;

    private PickaxeLevel myData;
    private PlayerStats playerStats;

    public void Setup(PickaxeLevel data, PlayerStats stats)
    {
        myData = data;
        playerStats = stats;
        
        priceText.text = "$" + data.cost;
    }

    void Update()
    {
        if (playerStats == null || myData == null) return;

        // Check if player can afford it
        if (playerStats.money >= myData.cost)
        {
            buyButtonImage.color = affordableColor;
        }
        else
        {
            buyButtonImage.color = expensiveColor;
        }
    }
}
