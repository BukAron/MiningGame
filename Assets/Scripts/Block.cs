using UnityEngine;

public class Block : MonoBehaviour
{
    public int maxHealth = 3;
    int health;

    Renderer rend;
    Color baseColor;
    AudioSource audioSource;

    void Awake()
    {
        health = maxHealth;

        rend = GetComponent<Renderer>();
        rend.material = new Material(rend.material); // avoid shared material bug
        baseColor = rend.material.color;
        audioSource = GetComponent<AudioSource>();
    }

    public void Mine(int damage)
    {
        health -= damage;
        UpdateOpacity();

        if (health <= 0)
        {
            PlayDestroySound();
            Destroy(gameObject);
        }
    }

    void UpdateOpacity()
    {
        float t = (float)health / maxHealth;

        Color c = baseColor;
        c.a = Mathf.Lerp(0.25f, 1f, t); // tweak if needed
        rend.material.color = c;
    }

    void PlayDestroySound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
        }
    }

}
