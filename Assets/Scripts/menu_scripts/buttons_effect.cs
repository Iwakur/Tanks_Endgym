using UnityEngine;

public class ButtonLookAtCursor : MonoBehaviour
{
    [SerializeField] private float rotationAmount = 10f;   // сила наклона
    [SerializeField] private float smoothSpeed = 5f;       // плавность

    private Quaternion startRotation;

    void Start()
    {
        startRotation = transform.rotation;
    }

    void Update()
    {
        // Берём позицию курсора
        Vector3 mousePos = Input.mousePosition;

        // Нормализуем координаты (от -1 до 1 по X и Y)
        float x = (mousePos.x / Screen.width - 0.5f) * 2f;
        float y = (mousePos.y / Screen.height - 0.5f) * 2f;

        // Целевой наклон
        Quaternion targetRotation = startRotation * Quaternion.Euler(-y * rotationAmount, -x * rotationAmount, 0);

        // Плавный поворот
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
    }
}
