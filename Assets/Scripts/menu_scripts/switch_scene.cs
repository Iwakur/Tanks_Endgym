using UnityEngine;
using UnityEngine.SceneManagement;

public class switch_scene : MonoBehaviour
{
    [SerializeField] private string credits; // ��� �����, ���� ����������

    void OnMouseDown()
    {
        // ��������� ��������� �����
        SceneManager.LoadScene(credits);
    }
}
