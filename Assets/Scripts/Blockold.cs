using UnityEngine;

public class Block : MonoBehaviour
{
    [HideInInspector] public int maxHealth = 3;
    [HideInInspector] public int pointValue = 1;
    int health;

    Renderer rend;
    Color baseColor;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        // OLD CODE HAD: rend.material = new Material(rend.material); <-- DELETE THIS
        
        // Use sharedMaterial to get the color without creating a copy
        baseColor = rend.sharedMaterial.color;
    }

    public void SetupBlock(int tierHealth, int tierValue)
    {
        maxHealth = tierHealth;
        health = maxHealth;
        pointValue = tierValue;
        UpdateOpacity();
    }

    public void Mine(int damage)
    {
        health -= damage;
        UpdateOpacity();

        if (health <= 0)
        {
            Terrain terrain = Object.FindFirstObjectByType<Terrain>();
            if (terrain != null)
            {
                terrain.AddToInventory(pointValue);
            }

            Destroy(gameObject);
        }
    }

    void UpdateOpacity()
    {
        if (rend == null) return;

        float t = (float)health / maxHealth;
        Color c = baseColor;
        c.a = Mathf.Lerp(0.25f, 1f, t); 
        
        // This is the "Old" way. It's fast, but only works well if 
        // GPU Instancing is checked on the material!
        rend.material.color = c; 
    }
}