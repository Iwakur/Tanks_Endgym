using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TankController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float boostMultiplier = 2f;
    public float rotateSpeed = 100f;
    public float accelSmooth = 5f;      // higher = snappier acceleration
    public float turnSmooth = 5f;       // higher = snappier turning

    [Header("Flip Recovery")]
    public float flipCheckDelay = 3f;
    public float uprightForce = 500f;
    private Rigidbody rb;
    private float lastUprightTime;

    // public Transform cameraTarget;
    // public float cameraSmooth = 5f;

    private float currentMoveInput;
    // private Quaternion smoothedCamRot;
    private float currentTurnInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastUprightTime = Time.time;
        // if (cameraTarget != null)
        //     smoothedCamRot = cameraTarget.rotation;
    }

    void Update()
    {
        HandleMovement();
        HandleFlipRecovery();

        // if (cameraTarget != null)
        // {
        //     Quaternion targetRot = transform.rotation;

        //     // Smooth toward tank’s rotation
        //     smoothedCamRot = Quaternion.Slerp(
        //         smoothedCamRot,
        //         targetRot,
        //         cameraSmooth * Time.deltaTime
        //     );

        //     cameraTarget.rotation = smoothedCamRot;
        // }
    }

    void HandleMovement()
    {
        float targetMove = Input.GetAxis("Vertical");
        float targetTurn = Input.GetAxis("Horizontal");

        // Smooth input for inertia
        currentMoveInput = Mathf.Lerp(currentMoveInput, targetMove, accelSmooth * Time.deltaTime);
        currentTurnInput = Mathf.Lerp(currentTurnInput, targetTurn, turnSmooth * Time.deltaTime);

        float speed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
            speed *= boostMultiplier;

        Vector3 move = transform.forward * currentMoveInput * speed * Time.deltaTime;
        rb.MovePosition(rb.position + move);

        Quaternion turnOffset = Quaternion.Euler(0, currentTurnInput * rotateSpeed * Time.deltaTime, 0);
        rb.MoveRotation(rb.rotation * turnOffset);
    }

    void HandleFlipRecovery()
    {
        if (Vector3.Dot(transform.up, Vector3.up) < 0.1f)
        {
            if (Time.time - lastUprightTime > flipCheckDelay)
            {
                rb.AddForce(Vector3.up * uprightForce);
                rb.MoveRotation(Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0));
            }
        }
        else
        {
            lastUprightTime = Time.time;
        }
    }
}
