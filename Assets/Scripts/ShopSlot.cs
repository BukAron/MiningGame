using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    [Header("References")]
    public Image buyButtonImage;
    public TextMeshProUGUI priceText;
    public Image pickaxeIcon; 

    [Header("Stat Texts")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI cooldownText;

    private PickaxeLevel myData;
    private PlayerStats playerStats;

    public void Setup(PickaxeLevel data, PlayerStats stats)
    {
        myData = data;
        playerStats = stats;
        
        if (nameText != null) nameText.text = data.name;
        if (damageText != null) damageText.text = "Damage: " + data.damage;
        if (rangeText != null) rangeText.text = "Range: " + data.range;
        if (cooldownText != null) cooldownText.text = "Cooldown: " + data.mineInterval + "s";
        if (priceText != null) priceText.text = "$" + data.cost;
    }

    void Update()
    {
        if (playerStats == null || myData == null) return;

    }
}