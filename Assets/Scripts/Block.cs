using UnityEngine;

public class Block : MonoBehaviour
{

    public int maxHealth = 3;
    int health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        health = maxHealth;
    }

    public void Mine(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
