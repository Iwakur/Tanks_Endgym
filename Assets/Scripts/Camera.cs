using UnityEngine;
using UnityEngine.UI;

public class CameraSwitcher : MonoBehaviour
{
    public Camera normalCam;
    public Camera scopeCam;
    public Image crosshairDot; // drag your dot here in inspector
    private bool isScoped;

    void Start()
    {
        // Start with normal cam enabled, scope disabled
        normalCam.enabled = true;
        scopeCam.enabled = false;
    }

    void Update()
    {
        crosshairDot.enabled = isScoped;

        if (Input.GetMouseButtonDown(1)) // right click
        {
            isScoped = !isScoped; // toggle

            normalCam.enabled = !isScoped;
            scopeCam.enabled = isScoped;
        }
    }
}
