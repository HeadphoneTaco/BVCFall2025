using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicBank : MonoBehaviour
{
    public AudioClip BackgroundClip;
    public AudioSource ShootAudioSource;

    public void Awake()
    {
        // Background
        var audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            gameObject.AddComponent<AudioSource>();
            Debug.Log("Creating missing AudioSource component for MusicBank in Music Bank.");
            return;
        }
        
        audioSource.clip = BackgroundClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void OnShootButtonClick()
    {
        if (ShootAudioSource != null)
        {
            ShootAudioSource.Play();
        }
    }
}