using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialoguePunk : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI pressE;
    public GameObject dialogueBox;
    public GameObject buyButton;
    public GameObject rubyPickaxe;
    public Transform player;
    public GameObject ventDoor;

    public void ShowDialogue()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PickaxeShop.IsAnyShopOpen = true;
        nameText.text = "Punk Prisoner";
        pressE.enabled = false;
        if(rubyPickaxe.activeSelf)
        {
            dialogueText.text = "About that way out... It will cost you $500,000 dollars if u pay it i will make sure you get home safely.";
            buyButton.SetActive(true);
        }
        else
        {
            dialogueText.text = "Hey why are u so small? Anyway i found a way out of here, but it will cost you a lot so before we talk get yourself a ruby pickaxe.";
            buyButton.SetActive(false);
        }
        
        dialogueBox.SetActive(true);
        dialogueText.enabled = true;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < 3f && Input.GetKeyDown(KeyCode.E))
        {
            ShowDialogue();
            pressE.enabled = true;
        }
        if (distance > 4f && dialogueBox.activeSelf)
        {
            CloseDialogue();
        }
    }

    public void BuyEscape()
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        if (stats.money >= 500000)
        {
            stats.AddMoney(-500000);
            dialogueText.text = "Thank you for your business, if you look to the top right of the cell you will see a vent that leads you out of here.";
            buyButton.SetActive(false);
            ventDoor.SetActive(false);
        }
    }

    public void CloseDialogue()
    {
        dialogueBox.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PickaxeShop.IsAnyShopOpen = false;
    }


}