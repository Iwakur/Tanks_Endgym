using UnityEngine;

public class ButtonLookAtCursor : MonoBehaviour
{
    [SerializeField] private float rotationAmount = 10f;   // сила наклона
    [SerializeField] private float smoothSpeed = 5f;       // плавность
    [SerializeField] private Transform cameraTransform;    // камера, к которой привязана кнопка

    private Quaternion localStartRotation;

    void Start()
    {
        // Запоминаем изначальный ЛОКАЛЬНЫЙ поворот относительно камеры
        localStartRotation = transform.localRotation;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        float x = (mousePos.x / Screen.width - 0.5f) * 2f;
        float y = (mousePos.y / Screen.height - 0.5f) * 2f;

        // Целевой поворот считается в локальных координатах кнопки
        Quaternion targetLocalRotation = localStartRotation * Quaternion.Euler(-y * rotationAmount, -x * rotationAmount, 0);

        // Применяем плавно в локальных координатах
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetLocalRotation, Time.deltaTime * smoothSpeed);
    }
}
