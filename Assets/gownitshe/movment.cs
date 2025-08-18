using UnityEngine;

public class TankController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 100f;

    void Update()
    {
        float move = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * move);

        float turn = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * turn);
    }
}
