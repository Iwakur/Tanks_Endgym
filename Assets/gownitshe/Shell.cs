using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f;
    public int damage = 25;

    void Start()
    {
        Destroy(gameObject, lifetime); // auto cleanup
    }

    void OnCollisionEnter(Collision collision)
    {
        TankHealth target = collision.gameObject.GetComponentInParent<TankHealth>();
        if (target != null)
        {
            // play damage sound from the tank, not the bullet
            TankAudio audio = collision.gameObject.GetComponentInParent<TankAudio>();
            if (audio != null)
                audio.PlayDamage();

            target.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
