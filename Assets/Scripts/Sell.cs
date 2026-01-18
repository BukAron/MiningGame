using UnityEngine;

public class Shop : MonoBehaviour
{
    public float interactionRange = 5f;
    public Transform playerTransform; 
    public Camera playerCam;
    public Terrain terrainScript;
    public GameObject interactUI;

    void Update()
    {
        bool isLookingAtShop = CheckIfLookingAtShop();

        
        if (isLookingAtShop)
        {
            interactUI.SetActive(true);

            
            if (Input.GetKeyDown(KeyCode.E))
            {
                SellBlocks();
            }
        }
        else
        {
            interactUI.SetActive(false);
        }
    }

    bool CheckIfLookingAtShop()
    {
        
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            
            if (hit.collider.gameObject == this.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    void SellBlocks()
    {
        int valueToPay = terrainScript.GetValueAndClear();

        if (valueToPay > 0)
        {
            PlayerStats stats = playerTransform.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.AddMoney(valueToPay);
            }
        }
    }
}