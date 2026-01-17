using UnityEngine;

public class PlayerCam : MonoBehaviour
{
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
    // We use Time.deltaTime to ensure that 10 degrees of movement 
    // takes the same amount of real-world time regardless of FPS.
    float mouseX = Input.GetAxisRaw("Mouse X") * sensX * 0.01f;
    float mouseY = Input.GetAxisRaw("Mouse Y") * sensY * 0.01f;

    yRotation += mouseX;
    xRotation -= mouseY;

    // Keep the player from looking behind their own back
    xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    // Apply rotations
    transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    orientation.rotation = Quaternion.Euler(0, yRotation, 0);
} 

}
