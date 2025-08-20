using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("References")]
    public Transform pivot;   // assign in inspector
    public Transform gun;     // assign in inspector

    [Header("Settings")]
    public float yawSpeed = 200f;     // deg/sec
    public float pitchSpeed = 30f;    // deg/sec
    public float minPitch = -5f;
    public float maxPitch = 30f;
    public bool lockAndHideCursor = true;


    public float minPitchAudio = 0.9f;
    public float maxPitchAudio = 1.1f;

    private float currentPitch;
    private bool isRotating;

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
        if (!lockAndHideCursor && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (pivot == null || gun == null) return;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        isRotating = false;

        // --- YAW (Turret rotation) ---
        if (Mathf.Abs(mouseX) > Mathf.Epsilon)
        {
            float yaw = mouseX * yawSpeed * Time.deltaTime;
            transform.RotateAround(pivot.position, pivot.up, yaw);
            isRotating = true;
        }

        // --- PITCH (Gun elevation) ---
        if (Mathf.Abs(mouseY) > Mathf.Epsilon)
        {
            currentPitch -= mouseY * pitchSpeed * Time.deltaTime;
            currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);

            gun.localRotation = Quaternion.Euler(currentPitch, 0f, 0f);
            isRotating = true;
        }

    }
}
