using UnityEngine;
using Mihir;
using Solo.MOST_IN_ONE;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource _audioSource;

    public AudioClip _waterSplashSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void PlayAudio(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    public void PlayWaterSplash()
    {
        PlayAudio(_waterSplashSound);
        SoftImpactHaptic();
    }

    // For vibration this Method Call
    public void SoftImpactHaptic()
    {
        MOST_HapticFeedback.Generate(MOST_HapticFeedback.HapticTypes.SoftImpact);
    }
}
