using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue1 : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public GameObject dialogueBox;
    public GameObject blocker;
    public Transform player;

    public void ShowDialogue()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PickaxeShop.IsAnyShopOpen = true;

        nameText.text = "Cellmate Joe";

        dialogueText.text = "Welcome to the Prison Cellmate, In this prison you need to mine to make money and survive. Let me give a few tips for you, First, you will find most of the valuble blocks deep down in the mine. Second, use the Button R to teleport up so you can sell your blocks.";
        blocker.SetActive(false);

        dialogueBox.SetActive(true);
        dialogueText.enabled = true;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < 3f && Input.GetKeyDown(KeyCode.E))
        {
            ShowDialogue();
        }
        if (distance > 4f && dialogueBox.activeSelf)
        {
            CloseDialogue();
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