using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f;
    public int damage = 25;
    public GameObject explosionPrefab;

    void Start()
    {
        Destroy(gameObject, lifetime); // auto cleanup
    }

    void OnCollisionEnter(Collision collision)
    {
        ModuleHitbox hitbox = collision.collider.GetComponent<ModuleHitbox>();
        if (hitbox != null)
        {
            hitbox.TakeDamage(damage);
        }

        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 3f);
        }

        Destroy(gameObject);
    }

}
