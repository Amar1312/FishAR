using UnityEngine;
using Mihir;

public class AudioHomeManager : Singleton<AudioHomeManager>
{
    public AudioSource _audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void PlayAudio(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
