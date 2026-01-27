using UnityEngine;

public class Minereset : MonoBehaviour
{
    public float interactionRange = 5f;
    public Camera playerCam;

    void Update()
    {
        if (CheckIfLookingAtShop())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Terrain terrain = Object.FindAnyObjectByType<Terrain>();
                if (terrain != null)
                {
                    terrain.ResetChunk();
                }
            }
        }
    }

    bool CheckIfLookingAtShop()
{
    if (playerCam == null) return false;

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