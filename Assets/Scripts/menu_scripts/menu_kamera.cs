using UnityEngine;

public class RotateAroundSelf : MonoBehaviour
{
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0, 30, 0);

    void Update()
    {
        // ������� ������ ������ ����� ��������� ���
        transform.Rotate(rotationSpeed * Time.deltaTime, Space.Self);
    }
}
