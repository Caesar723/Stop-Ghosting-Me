using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;          // Singleton instance
    public AudioSource musicSource;               // Music audio source
    public AudioSource environmentSource;         // Environment audio source (optional, for sound effects)

    public AudioSource MusicSource { get => musicSource; set => musicSource = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);        // Persist across scenes
        }
        else
        {
            Destroy(gameObject);                  
        }
    }

    public void SetMusicVolume(float volume)
    {
        MusicSource.volume = volume;
    }

    public void SetEnvironmentVolume(float volume)
    {
        if (environmentSource != null)
            environmentSource.volume = volume;
    }
}
