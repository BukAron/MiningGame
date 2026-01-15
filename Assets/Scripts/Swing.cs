using UnityEngine;

public class Swing : MonoBehaviour
{
    Animator swing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        swing = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            swing.SetTrigger("Swing");
            Debug.Log("Swinging");
        }
    }
}