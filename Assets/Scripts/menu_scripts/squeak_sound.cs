using UnityEngine;

public class ObjectHoverSound : MonoBehaviour
{
    public AudioClip hoverSound;       // Твой звук
    private AudioSource audioSource;
    private bool hasPlayed = false;    // чтобы звук не играл бесконечно

    void Start()
    {
        // Добавляем или берём AudioSource
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
        hasPlayed = false; // сбрасываем, чтобы снова сработало
    }
}
