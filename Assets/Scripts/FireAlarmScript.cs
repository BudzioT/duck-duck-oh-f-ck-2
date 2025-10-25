using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class FireAlarmScript : MonoBehaviour
{
    public AudioClip[] startSounds;
    private AudioSource audioSource;
    public AudioSource alarmAudio;
    public AudioClip extinguishSound;
    public Image overlayImage;
    public float pulseSpeed = 2f;
    public float maxAlpha = 0.4f;
    public float minAlpha = 0.1f;
    private bool isActive = false;
    private float pulseTimer = 0f;
    public FireEmojiManager emojiManager;

    void Start()
    {
        if (overlayImage == null)
            overlayImage = GetComponent<Image>();
        SetAlpha(0f);
    }

    void Update()
    {
        if (!isActive) return;

        pulseTimer += Time.deltaTime * pulseSpeed;
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, (Mathf.Sin(pulseTimer) + 1f) / 2f);
        SetAlpha(alpha);
    }

    void SetAlpha(float alpha)
    {
        if (overlayImage != null)
        {
            Color c = overlayImage.color;
            c.a = alpha;
            overlayImage.color = c;
        }
    }

    public void ActivateAlarm()
    {
        Debug.Log("ActivateAlarm called!");
        isActive = true;

        if (startSounds != null && startSounds.Length > 0)
        {
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();
            AudioClip clip = startSounds[Random.Range(0, startSounds.Length)];
            audioSource.PlayOneShot(clip);
        }

        if (alarmAudio != null)
        {
            alarmAudio.loop = true;
            alarmAudio.Play();
        }

        if (emojiManager != null)
            emojiManager.Activate();
    }

    public void DeactivateAlarm()
    {
        Debug.Log("DeactivateAlarm called!");
        isActive = false;
        SetAlpha(0f);

        if (alarmAudio != null)
        {
            alarmAudio.Stop();
        }

        if (extinguishSound != null)
        {
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();

            audioSource.PlayOneShot(extinguishSound);
        }

        if (emojiManager != null)
            emojiManager.Deactivate();
        else
            Debug.LogWarning("EmojiManager is null!");
    }
}