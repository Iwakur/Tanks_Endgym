using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera normalCam;
    public Camera scopeCam;

    private bool isScoped;

    void Start()
    {
        // Start with normal cam enabled, scope disabled
        normalCam.enabled = true;
        scopeCam.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // right click
        {
            isScoped = !isScoped; // toggle

            normalCam.enabled = !isScoped;
            scopeCam.enabled = isScoped;
        }
    }
}
