using UnityEngine;

public class AimMarker : MonoBehaviour
{
    public Transform firePoint;        // barrel tip or gun muzzle
    public GameObject markerPrefab;    // drag the tiny dot prefab here
    private GameObject marker;

    void Start()
    {
        marker = Instantiate(markerPrefab);
    }

    void Update()
    {
        RaycastHit hit;
        // Cast a ray forward from the gun
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 1000f))
        {
            marker.transform.position = hit.point;
            marker.SetActive(true);
        }
        else
        {
            marker.SetActive(false);
        }
    }
}
