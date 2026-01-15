using UnityEngine;

public class Swing : MonoBehaviour
{
    Animator mine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mine = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mine.SetTrigger("Swing");
        }
    }
}
