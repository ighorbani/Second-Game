using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    public int damage = 1;
    public GameObject soundObject;
    public GameObject playerShootingParticle;
    
    void Start()
    {
        // Destroy the bullet after a certain lifetime
        Destroy(gameObject, lifetime);
        Instantiate(soundObject, transform.position, transform.rotation);
    }

    void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        float newScale = transform.localScale.x + .2f * Time.deltaTime;
        transform.localScale = new Vector3(newScale, newScale, 1f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Magician magician = other.GetComponent<Magician>();

            if (magician != null)
            {
                magician.TakeDamage(damage);
            }
        }

        // Instantiate particle effect
        InstantiateParticleEffect();

        // Destroy the bullet
        Destroy(gameObject);
    }

    void InstantiateParticleEffect()
    {
        Instantiate(playerShootingParticle, transform.position, Quaternion.identity);
    }
}