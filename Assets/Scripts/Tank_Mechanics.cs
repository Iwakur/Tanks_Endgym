using UnityEngine;
using System.Collections;
using TMPro; // only if it's TextMeshPro, not old 3D TextMesh

public class TankMechanics : MonoBehaviour
{
    [Header("Hull")]
    public int hullHP = 300;
    private bool isDestroyed = false;
    [Header("UI")]
    public TMP_Text hpText;   // drag your TextMeshPro object here

    [Header("Status flags")]
    public bool isOnFire = false;
    public bool tracksBroken = false;
    public bool engineDead = false;
    public bool opticsDamaged = false;
    public bool turretDamaged = false;

    [Header("Effects")]
    public GameObject explosionEffectPrefab;
    public GameObject fireEffectPrefab;
    public GameObject tracksSmokePrefab;

    private GameObject fireInstance;
    private GameObject tracksSmokeInstance;

    [Header("Effect Anchors")]
    public Transform hullAnchor;     // e.g. center of hull
    public Transform tracksAnchor;   // e.g. where smoke should appear
    public Transform engineAnchor;   // e.g. where fire should appear


    void Update()
    {
        if (hpText != null)
        {
            hpText.text = hullHP.ToString();  // just shows "300", "299", etc.
        }
    }
    void Start()
    {
  
        if (fireEffectPrefab != null && engineAnchor != null)
        {
            fireInstance = Instantiate(fireEffectPrefab, engineAnchor);
            fireInstance.transform.localPosition = Vector3.zero;
            fireInstance.transform.localRotation = Quaternion.identity;
            fireInstance.SetActive(false);

        }
        else
        {
        }

        if (tracksSmokePrefab != null && tracksAnchor != null)
        {
            tracksSmokeInstance = Instantiate(tracksSmokePrefab, tracksAnchor);
            tracksSmokeInstance.transform.localPosition = Vector3.zero;
            tracksSmokeInstance.transform.localRotation = Quaternion.identity;
            tracksSmokeInstance.SetActive(false);
            
        }
        else
        {
        }
    }
    public void ApplyHullDamage(int dmg)
    {
        if (isDestroyed) return;

        hullHP -= dmg;
        Debug.Log($"Hull took {dmg}, HP now {hullHP}");

        if (hullHP <= 0)
            Die();
    }

    public void HitModule(TankModule module, int baseDamage)
    {
        if (isDestroyed) return;

        float roll = Random.value;

        switch (module)
        {
            case TankModule.Hull:
                ApplyHullDamage(baseDamage);
                break;

            case TankModule.Ammo:
                if (roll < 0.1f) // 10% instant death
                {
                    Debug.Log("Ammo cookoff! Instant death.");
                    Die();
                }
                else
                {
                    Debug.Log("Ammo hit: massive hull damage!");
                    ApplyHullDamage(Mathf.RoundToInt(baseDamage * 2f));
                }
                break;

            case TankModule.Engine:
                if (roll < 0.4f) // 40% fire
                {
                    Debug.Log("Engine fire started!");
                    isOnFire = true;
                    if (fireInstance != null) fireInstance.SetActive(true);
                    StartCoroutine(FireCountdown());
                }
                else
                {
                    Debug.Log("Engine hit hard: big hull damage!");
                    ApplyHullDamage(Mathf.RoundToInt(baseDamage * 1.5f));
                }
                break;

            case TankModule.Tracks:
                if (roll < 0.8f) // 80% chance to break
                {
                    Debug.Log("Tracks broken! Smoke active.");
                    tracksBroken = true;
                    if (tracksSmokeInstance != null) tracksSmokeInstance.SetActive(true);
                }
                else
                {
                    Debug.Log("Tracks grazed, minor hull damage.");
                    ApplyHullDamage(baseDamage / 2);
                }
                break;

            case TankModule.Optics:
                if (roll < 0.3f)
                {
                    Debug.Log("Optics damaged → blur.");
                    opticsDamaged = true;
                }
                else
                    ApplyHullDamage(baseDamage);
                break;

            case TankModule.Turret:
                if (roll < 0.2f)
                {
                    Debug.Log("Turret jammed → slower reload.");
                    turretDamaged = true;
                }
                else
                {
                    Debug.Log("Turret hit → partial hull damage.");
                    ApplyHullDamage(baseDamage / 2);
                }
                break;
        }
    }

    private IEnumerator FireCountdown()
    {
        yield return new WaitForSeconds(10f);
        if (isOnFire)
        {
            Debug.Log("Tank burned down!");
            Die();
        }
    }

    private void Die()
    {
        if (isDestroyed) return;
        isDestroyed = true;

        Debug.Log($"{gameObject.name} destroyed!");

        if (explosionEffectPrefab != null)
        {
    
            GameObject boom = Instantiate(explosionEffectPrefab, hullAnchor.position, hullAnchor.rotation);
            Destroy(boom, 5f);

        }

        Destroy(gameObject, 1f);
    }
}
