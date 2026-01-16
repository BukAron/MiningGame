using UnityEngine;
using PixelReyn.VoxelSeries.Part2; // Add this namespace to see Container

public class Pick : MonoBehaviour
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
        if(animator != null) animator.SetTrigger("Swing");

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            // 1. Check if we hit the Voxel Container
            Container container = hit.collider.GetComponent<Container>();
            
            if (container != null)
            {
                // Send the exact hit point and normal so the container calculates which block
                container.DamageVoxel(hit.point, hit.normal, damage);
            }
            
            // Optional: Keep support for old blocks if you still use them
            else 
            {
                Block block = hit.collider.GetComponent<Block>();
                if (block != null) block.Mine(damage);
            }
        }
    }
}
