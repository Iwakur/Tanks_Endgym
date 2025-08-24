using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody))]
public class TankController : NetworkBehaviour
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

    private float currentMoveInput;
    private float currentTurnInput;

    // Mirror sync — server is the boss, updates all clients
    [SyncVar] private Vector3 syncPosition;
    [SyncVar] private Quaternion syncRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastUprightTime = Time.time;
    }

    void Update()
    {
        if (isLocalPlayer) // only our tank reads input
        {
            HandleMovement();
            HandleFlipRecovery();

            // tell server our new state
            CmdSendTransform(rb.position, rb.rotation);
        }
        else
        {
            // other players' tanks → follow synced values
            rb.position = Vector3.Lerp(rb.position, syncPosition, Time.deltaTime * 10f);
            rb.rotation = Quaternion.Lerp(rb.rotation, syncRotation, Time.deltaTime * 10f);
        }
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

    // Tell the server our transform
    [Command]
    void CmdSendTransform(Vector3 pos, Quaternion rot)
    {
        syncPosition = pos;
        syncRotation = rot;
    }
}
