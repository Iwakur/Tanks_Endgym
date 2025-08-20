using UnityEngine;

public class TankAudio : MonoBehaviour
{
    [Header("Clips")]
    public AudioClip rollingSound;
    public AudioClip headRotateSound;
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip damageSound;

    [Header("Settings")]
    [Range(0f, 1f)] public float rollingVolume = 1f;
    [Range(0f, 1f)] public float rotateVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;
    public float fadeSpeed = 5f;

    private AudioSource rollingSource;
    private AudioSource rotateSource;
    private AudioSource sfxSource;

    void Awake()
    {
        // Rolling (engine)
        rollingSource = gameObject.AddComponent<AudioSource>();
        rollingSource.loop = true;
        rollingSource.playOnAwake = false;
        rollingSource.clip = rollingSound;

        // Rotating (turret)
        rotateSource = gameObject.AddComponent<AudioSource>();
        rotateSource.loop = true;
        rotateSource.playOnAwake = false;
        rotateSource.clip = headRotateSound;

        // One-shot SFX (shots, reloads, damage)
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
    }

    void Update()
    {
        HandleRolling();
        HandleRotation();
    }

    void HandleRolling()
    {
        bool moving = Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f
                   || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f;

        float targetVol = moving ? rollingVolume : 0f;

        if (moving && !rollingSource.isPlaying)
            rollingSource.Play();

        rollingSource.volume = Mathf.Lerp(rollingSource.volume, targetVol, fadeSpeed * Time.deltaTime);

        if (!moving && rollingSource.volume < 0.01f)
            rollingSource.Stop();
    }

    void HandleRotation()
    {
        bool rotating = Mathf.Abs(Input.GetAxis("Mouse X")) > 0.1f;

        float targetVol = rotating ? rotateVolume : 0f;

        if (rotating && !rotateSource.isPlaying)
            rotateSource.Play();

        rotateSource.volume = Mathf.Lerp(rotateSource.volume, targetVol, fadeSpeed * Time.deltaTime);

        if (!rotating && rotateSource.volume < 0.01f)
            rotateSource.Stop();
    }

    // --- Public API ---
    public void PlayShot() => sfxSource.PlayOneShot(shotSound, sfxVolume);
    public void PlayReload() => sfxSource.PlayOneShot(reloadSound, sfxVolume);
    public void PlayDamage() => sfxSource.PlayOneShot(damageSound, sfxVolume);
}
