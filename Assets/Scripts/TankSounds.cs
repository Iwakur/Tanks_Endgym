using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TankAudio : MonoBehaviour
{
    [Header("Clips")]
    public AudioClip rollingSound;
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip damageSound;
    public AudioClip headRotateSound;

    [Header("Settings")]
    public float fadeSpeed = 5f; // how fast rolling/rotation fade in/out

    private AudioSource loopSource;   // for rolling & turret hum
    private AudioSource sfxSource;    // for shots/reload/damage
    private bool isRolling;
    private bool isRotating;

    void Awake()
    {
        // Create 2 audio sources
        loopSource = gameObject.AddComponent<AudioSource>();
        loopSource.loop = true;
        loopSource.playOnAwake = false;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
    }

    void Update()
    {
        HandleRollingSound();
        HandleHeadRotateSound();
    }

    void HandleRollingSound()
    {
        bool moving = Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f;

        if (moving)
        {
            if (!isRolling)
            {
                loopSource.clip = rollingSound;
                loopSource.volume = 0f;
                loopSource.Play();
                isRolling = true;
            }
            loopSource.volume = Mathf.Lerp(loopSource.volume, 1f, fadeSpeed * Time.deltaTime);
        }
        else if (isRolling)
        {
            loopSource.volume = Mathf.Lerp(loopSource.volume, 0f, fadeSpeed * Time.deltaTime);
            if (loopSource.volume < 0.05f)
            {
                loopSource.Stop();
                isRolling = false;
            }
        }
    }

    void HandleHeadRotateSound()
    {
        // Detect turret rotation input (replace if you control turret differently)
        bool rotating = Mathf.Abs(Input.GetAxis("Mouse X")) > 0.1f;

        if (rotating)
        {
            if (!isRotating)
            {
                loopSource.clip = headRotateSound;
                loopSource.volume = 0f;
                loopSource.Play();
                isRotating = true;
            }
            loopSource.volume = Mathf.Lerp(loopSource.volume, 1f, fadeSpeed * Time.deltaTime);
        }
        else if (isRotating)
        {
            loopSource.volume = Mathf.Lerp(loopSource.volume, 0f, fadeSpeed * Time.deltaTime);
            if (loopSource.volume < 0.05f)
            {
                loopSource.Stop();
                isRotating = false;
            }
        }
    }

    // --- Public API for other scripts ---
    public void PlayShot() => sfxSource.PlayOneShot(shotSound);
    public void PlayReload() => sfxSource.PlayOneShot(reloadSound);
    public void PlayDamage() => sfxSource.PlayOneShot(damageSound);
}
