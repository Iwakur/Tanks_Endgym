using UnityEngine;

public class ButtonLookAtCursor : MonoBehaviour
{
    [SerializeField] private float rotationAmount = 10f;   // ���� �������
    [SerializeField] private float smoothSpeed = 5f;       // ���������

    private Quaternion startRotation;

    void Start()
    {
        startRotation = transform.rotation;
    }

    void Update()
    {
        // ���� ������� �������
        Vector3 mousePos = Input.mousePosition;

        // ����������� ���������� (�� -1 �� 1 �� X � Y)
        float x = (mousePos.x / Screen.width - 0.5f) * 2f;
        float y = (mousePos.y / Screen.height - 0.5f) * 2f;

        // ������� ������
        Quaternion targetRotation = startRotation * Quaternion.Euler(-y * rotationAmount, -x * rotationAmount, 0);

        // ������� �������
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
    }
}
