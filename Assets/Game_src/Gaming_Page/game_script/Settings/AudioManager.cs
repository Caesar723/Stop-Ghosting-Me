using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;          // Singleton instance
    public AudioSource musicSource;               // Music audio source
    public AudioSource environmentSource;         // Environment audio source (optional, for sound effects)

    private const string MusicVolumeKey = "MusicVolume";
    private const string EnvironmentVolumeKey = "EnvironmentVolume";

    public AudioSource MusicSource { get => musicSource; set => musicSource = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Verify and initialize AudioSource references if missing
            if (musicSource == null)
            {
                musicSource = gameObject.AddComponent<AudioSource>();
                Debug.LogWarning("MusicSource was missing, added dynamically.");
            }
            if (environmentSource == null)
            {
                environmentSource = gameObject.AddComponent<AudioSource>();
                Debug.LogWarning("EnvironmentSource was missing, added dynamically.");
            }
            LoadVolumeSettings();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }



    public void SetMusicVolume(float volume)
    {
        MusicSource.volume = volume;
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
        PlayerPrefs.Save();
    }

    public void SetEnvironmentVolume(float volume)
    {
        if (environmentSource != null)
        {
            environmentSource.volume = volume;
            PlayerPrefs.SetFloat(EnvironmentVolumeKey, volume);
            PlayerPrefs.Save();
        }
    }

    private void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey(MusicVolumeKey))
            MusicSource.volume = PlayerPrefs.GetFloat(MusicVolumeKey);

        if (environmentSource != null && PlayerPrefs.HasKey(EnvironmentVolumeKey))
            environmentSource.volume = PlayerPrefs.GetFloat(EnvironmentVolumeKey);
    }


    public void PlayMusic(AudioClip newMusicClip)
    {
        if (musicSource == null)
        {
            Debug.LogError("MusicSource is null. Please assign it in the inspector.");
            return;
        }

        if (newMusicClip == null)
        {
            Debug.LogError("NewMusicClip is null. Please assign it before calling PlayMusic.");
            return;
        }

        Debug.Log($"Playing music clip: {newMusicClip.name}");
        musicSource.clip = newMusicClip;
        musicSource.Play();
    }


    public void PlayEnvironmentSound(AudioClip clip)
    {
        if (environmentSource != null && clip != null)
        {
            environmentSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("EnvironmentSource or clip is missing.");
        }
    }


}
