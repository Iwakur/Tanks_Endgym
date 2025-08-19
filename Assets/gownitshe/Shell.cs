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
        if (target != null ) 
        {

            target.TakeDamage(damage);
        }
      

        // Always destroy bullet after hit (or after some seconds)
        Destroy(gameObject);
    }
}
