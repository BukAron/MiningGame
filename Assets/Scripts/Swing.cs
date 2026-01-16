using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    [Header("Mining")]
    public int damage = 1;
    public float range = 4f;
    public float mineInterval = 0.3f;

    [Header("References")]
    public Camera cam;
    public Animator animator;

    private float mineTimer;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mineTimer -= Time.deltaTime;
            
            if (mineTimer <= 0f)
            {
                SwingAndMine();
                mineTimer = mineInterval;
            }
        }
        else
        {
            mineTimer = 0f;
        }
    }

    void SwingAndMine()
    {
        // Play animation
        animator.SetTrigger("Swing");

        // Raycast
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Block block = hit.collider.GetComponent<Block>();
            
            if (block != null)
            {
                block.Mine(damage);
            }
        }
    }
}