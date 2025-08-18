using UnityEngine;

public class TurretController : MonoBehaviour
{
    public float rotationSpeed = 100f; // �������� ��������

    void Update()
    {
        // �������� �������� ���� �� ��� X
        float mouseX = Input.GetAxis("Mouse X");

        // ������� ����� ������ ������������ ��� (Y)
        transform.Rotate(Vector3.up * mouseX * rotationSpeed * Time.deltaTime);
    }
}
