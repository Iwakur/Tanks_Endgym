using UnityEngine;
using UnityEngine.SceneManagement;

public class to_credits : MonoBehaviour
{
    [SerializeField] private string credits; // ��� �����, ���� ����������

    void OnMouseDown()
    {
        // ��������� ��������� �����
        SceneManager.LoadScene(credits);
    }
}
