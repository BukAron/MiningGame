using UnityEngine;

public class Minereset : MonoBehaviour
{


    public float interactionRange = 5f;
    public Transform playerTransform; 
    public Camera playerCam;
    public GameObject interactUI;


    void Update()
    {

            bool isLookingAtShop = CheckIfLookingAtShop();
            
            if (isLookingAtShop)
        {
            interactUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Terrain terrain = Object.FindAnyObjectByType<Terrain>();
                if (terrain != null)
                {
                    terrain.ResetChunk();
                }
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
}
