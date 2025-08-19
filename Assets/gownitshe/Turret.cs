using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Center of rotation. If left empty, a child named 'Pivot' (case-insensitive) will be used.")]
    public Transform pivot;

    [Header("Settings")]
    public float yawSpeed = 120f;          // degrees per second
    public bool lockAndHideCursor = true;  // because menus are for cowards

    void Awake()
    {
        // Auto-find a child named "Pivot" if you didn't drag one in
        if (pivot == null)
        {
            foreach (Transform t in GetComponentsInChildren<Transform>(true))
            {
                if (string.Equals(t.name, "Pivot", System.StringComparison.OrdinalIgnoreCase))
                {
                    pivot = t;
                    break;
                }
            }
        }

        if (pivot == null)
            Debug.LogError("[TurretController] No pivot assigned and no child named 'Pivot' found.", this);
    }

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
        // Quick unlock for testing
        if (lockAndHideCursor && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (pivot == null) return;

        float mouseX = Input.GetAxis("Mouse X");
        if (Mathf.Abs(mouseX) > Mathf.Epsilon)
        {
            float angle = mouseX * yawSpeed * Time.deltaTime;
            // Rotate *morda* around the pivot's UP axis (local Y)
            transform.RotateAround(pivot.position, pivot.up, angle);
        }
    }
}
