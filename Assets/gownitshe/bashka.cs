using UnityEngine;

public class TurretController : MonoBehaviour
{
    public float rotationSpeed = 100f; // скорость поворота

    void Update()
    {
        // ѕолучаем движение мыши по оси X
        float mouseX = Input.GetAxis("Mouse X");

        // ¬ращаем башню вокруг вертикальной оси (Y)
        transform.Rotate(Vector3.up * mouseX * rotationSpeed * Time.deltaTime);
    }
}
