using UnityEngine;

public class HideNearCamera : MonoBehaviour
{
    public Camera targetCamera;
    public float distanceThreshold = 3f;

    private Renderer _renderer;

    void Start()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;

        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (_renderer == null || targetCamera == null) return;

        float distance = Vector3.Distance(targetCamera.transform.position, transform.position);

        // Если камера близко — выключаем рендер объекта
        if (distance < distanceThreshold)
            _renderer.enabled = false;
        else
            _renderer.enabled = true;
    }
}
