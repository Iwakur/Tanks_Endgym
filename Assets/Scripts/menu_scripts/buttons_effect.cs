using UnityEngine;

public class ButtonLookAtCursor : MonoBehaviour
{
    [SerializeField] private float rotationAmount = 10f;   // ���� �������
    [SerializeField] private float smoothSpeed = 5f;       // ���������
    [SerializeField] private Transform cameraTransform;    // ������, � ������� ��������� ������

    private Quaternion localStartRotation;

    void Start()
    {
        // ���������� ����������� ��������� ������� ������������ ������
        localStartRotation = transform.localRotation;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        float x = (mousePos.x / Screen.width - 0.5f) * 2f;
        float y = (mousePos.y / Screen.height - 0.5f) * 2f;

        // ������� ������� ��������� � ��������� ����������� ������
        Quaternion targetLocalRotation = localStartRotation * Quaternion.Euler(-y * rotationAmount, -x * rotationAmount, 0);

        // ��������� ������ � ��������� �����������
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetLocalRotation, Time.deltaTime * smoothSpeed);
    }
}
