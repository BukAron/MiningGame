using UnityEngine;
using PixelReyn.VoxelSeries.Part2;
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
            Container container = hit.collider.GetComponent<Container>();
            if (container != null)
            {

                container.DamageVoxel(hit.point, hit.normal, damage);
            }
            
            else 
            {
                Block block = hit.collider.GetComponent<Block>();
                if (block != null) block.Mine(damage);
            }
        }
    }
}
