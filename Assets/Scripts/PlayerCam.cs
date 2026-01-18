using UnityEngine;

public class PlayerCam : MonoBehaviour


{

    public PickaxeShop shopScript;
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
{
    if (PickaxeShop.IsAnyShopOpen)
        {
            return;
        }

    float mouseX = Input.GetAxisRaw("Mouse X") * sensX * 0.01f;
    float mouseY = Input.GetAxisRaw("Mouse Y") * sensY * 0.01f;

    yRotation += mouseX;
    xRotation -= mouseY;

    xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    orientation.rotation = Quaternion.Euler(0, yRotation, 0);
} 

}
