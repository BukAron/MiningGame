using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    [Header("Mining")]
    public int damage = 1;
    public float range = 4f;
    public float mineInterval = 0.3f;
    public GameObject PickaxeModel;
    public GameObject currentPickaxeModel;
    public AudioSource mineSound;

    [Header("References")]
    public Camera cam;
    public Animator animator;

    private float mineTimer;

    void Update()
    {


        if (mineTimer > 0)
        {
            mineTimer -= Time.deltaTime;
        }

        if (Input.GetMouseButton(0))
        {
            
            if (mineTimer <= 0f)
            {
                SwingAndMine();
                mineTimer = mineInterval;
            }
        }
        
    }

    void SwingAndMine()
    {
        animator.SetTrigger("Swing");

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Block block = hit.collider.GetComponent<Block>();
            
            if (block != null)
            {
                block.Mine(damage);
                satifyingSound();

            }
        }
    }

    void satifyingSound()
    {
        float pitch = Random.Range(0.8f, 1.2f);
        mineSound.pitch = pitch;
        float volume = Random.Range(0.8f, 1.0f);

        mineSound.PlayOneShot(mineSound.clip, volume);


    }
}