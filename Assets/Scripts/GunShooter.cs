using UnityEngine;

public class GunShooter : MonoBehaviour
{
    [Header("References")]
    public Transform muzzle;       // empty object at barrel tip
    public GameObject bulletPrefab;
    public TankAudio tankAudio;    // drag your TankAudio component here

    [Header("Settings")]
    public float bulletForce = 1000f;
    public float fireRate = 1f;

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;

            if (tankAudio != null)
                tankAudio.PlayShot();
        }
    }

    void Shoot()
    {
        // Spawn the bullet rotated 180° on Y
        Quaternion rotated = muzzle.rotation * Quaternion.Euler(0, 180f, 0);
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, rotated);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(muzzle.forward * bulletForce);
    }
}
