using UnityEngine;
using UnityEngine.SceneManagement;

public class to_menu : MonoBehaviour
{
    [SerializeField] private string game_menu; // ��� �����, ���� ����������

    void OnMouseDown()
    {
        // ��������� ��������� �����
        SceneManager.LoadScene(game_menu);
    }
}
