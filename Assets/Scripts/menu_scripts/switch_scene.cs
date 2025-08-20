using UnityEngine;
using UnityEngine.SceneManagement;

public class switch_scene : MonoBehaviour
{
    [SerializeField] private string credits; // имя сцены, куда переносить

    void OnMouseDown()
    {
        // Загружаем указанную сцену
        SceneManager.LoadScene(credits);
    }
}
