using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public int money = 0;
    public TextMeshProUGUI moneyText;
    
    // Reference to your actual Pickaxe script on the player model
    public Pickaxe currentPickaxeScript; 

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyUI();
    }

    public bool TryBuyUpgrade(PickaxeLevel newLevel)
    {
        if (money >= newLevel.cost)
        {
            money -= newLevel.cost;
            UpdateMoneyUI();

            // Apply the new values to the actual pickaxe tool
            currentPickaxeScript.damage = newLevel.damage;
            currentPickaxeScript.range = newLevel.range;
            currentPickaxeScript.mineInterval = newLevel.mineInterval;
            
            Debug.Log("Upgraded to " + newLevel.name);
            return true;
        }
        
        Debug.Log("Not enough money!");
        return false;
    }

    void UpdateMoneyUI()
    {
        if (moneyText != null) moneyText.text = "Money: $" + money;
    }
}