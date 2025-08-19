using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Center of rotation. If left empty, a child named 'Pivot' (case-insensitive) will be used.")]
    public Transform pivot;   // Hinge for gun elevation
    public Transform gun;     // The Gun object (child of Pivot)

    [Header("Settings")]
    public float yawSpeed = 200f;     // deg/sec
    public float pitchSpeed = 30f;    // deg/sec
    public float minPitch = -5f;      // down limit
    public float maxPitch = 30f;      // up limit
    public bool lockAndHideCursor = true;

    private float currentPitch;

    
    void OnEnable()
    {
        if (lockAndHideCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void OnDisable()
    {
        if (lockAndHideCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void Update()
    {
        // ESC unlock for testing
        if (lockAndHideCursor && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (pivot == null || gun == null) return;

        // --- YAW (Turret rotation) ---
        float mouseX = Input.GetAxis("Mouse X");
        if (Mathf.Abs(mouseX) > Mathf.Epsilon)
        {
            float yaw = mouseX * yawSpeed * Time.deltaTime;
            transform.RotateAround(pivot.position, pivot.up, yaw);
        }

        // --- PITCH (Gun elevation) ---
        float mouseY = Input.GetAxis("Mouse Y");
        if (Mathf.Abs(mouseY) > Mathf.Epsilon)
        {
            currentPitch -= mouseY * pitchSpeed * Time.deltaTime;
            currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);

            gun.localRotation = Quaternion.Euler(currentPitch, 0f, 0f);
        }
    }
}
