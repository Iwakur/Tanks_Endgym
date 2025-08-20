using UnityEngine;
using UnityEngine.SceneManagement;

public class to_menu : MonoBehaviour
{
    [SerializeField] private string game_menu; // имя сцены, куда переносить

    void OnMouseDown()
    {
        // Загружаем указанную сцену
        SceneManager.LoadScene(game_menu);
    }
}
