using UnityEngine;

public class GunShooter : MonoBehaviour
{
    [Header("References")]
    public Transform muzzle;       // empty object at barrel tip
    public GameObject bulletPrefab;
    public TankAudio tankAudio;    // drag your TankAudio component here

    [Header("Settings")]
    public float bulletForce = 1000f;
    public float fireRate = 1f;       // shots per second
    public float reloadTime = 2f;     // seconds to reload
    public int magazineSize = 5;      // how many shots before reload
    public GameObject muzzleFlashPrefab;

    private float nextFireTime = 0f;
    private int currentAmmo;
    private bool isReloading = false;

    void Start()
    {
        currentAmmo = magazineSize;
    }

    void Update()
    {
        // If reloading, block input
        if (isReloading)
            return;

        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            if (currentAmmo > 0)
            {
                Shoot();
                currentAmmo--;
                nextFireTime = Time.time + 1f / fireRate;

                if (tankAudio != null)
                    tankAudio.PlayShot();
            }
            else
            {
                Shoot();

                // start reload if empty
                StartCoroutine(Reload());
            }
        }
    }

    void Shoot()
    {
        // Spawn the bullet rotated 180° on Y
        Quaternion rotated = muzzle.rotation * Quaternion.Euler(0, 180f, 0);
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, rotated);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(muzzle.forward * bulletForce);


        if (muzzleFlashPrefab != null)
        {
            GameObject flash = Instantiate(muzzleFlashPrefab, muzzle.position, muzzle.rotation, muzzle);
            Destroy(flash, 0.3f); // clean up after a short time
        }

    }

    System.Collections.IEnumerator Reload()
    {
        isReloading = true;

        if (tankAudio != null)
            tankAudio.PlayReload();

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = magazineSize;
        isReloading = false;
    }
}