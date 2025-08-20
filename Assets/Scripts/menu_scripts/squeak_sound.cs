using UnityEngine;

public class ObjectHoverSound : MonoBehaviour
{
    public AudioClip hoverSound;       // ���� ����
    private AudioSource audioSource;
    private bool hasPlayed = false;    // ����� ���� �� ����� ����������

    void Start()
    {
        // ��������� ��� ���� AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    void OnMouseEnter()
    {
        if (!hasPlayed && hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
            hasPlayed = true;
        }
    }

    void OnMouseExit()
    {
        hasPlayed = false; // ����������, ����� ����� ���������
    }
}
