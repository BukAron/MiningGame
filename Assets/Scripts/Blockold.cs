using UnityEngine;

public class Block : MonoBehaviour
{
    public int maxHealth = 3;
    public int scoreValue = 1;
    int health;

    Renderer rend;
    Color baseColor;
    

    void Awake()
    {
        health = maxHealth;

        rend = GetComponent<Renderer>();
        rend.material = new Material(rend.material); // avoid shared material bug
        baseColor = rend.material.color;
        
    }

    public void Mine(int damage)
    {
        health -= damage;
        UpdateOpacity();
        if (health <= 0)
        {
            Terrain terrain = Object.FindAnyObjectByType<Terrain>();
            if (terrain != null)
            {
                // We pass the scoreValue (1 or 100) to the terrain
                terrain.AddToInventory(scoreValue);
            }

            Destroy(gameObject);
        }
    }


    void UpdateOpacity()
    {
        float t = (float)health / maxHealth;

        Color c = baseColor;
        c.a = Mathf.Lerp(0.25f, 1f, t); // tweak if needed
        rend.material.SetColor("_BaseColor", c);
    }

}
