using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider environmentSlider;

    private void Start()
    {
        if (AudioManager.instance != null)
        {
            musicSlider.value = AudioManager.instance.MusicSource.volume;
            if (AudioManager.instance.environmentSource != null)
                environmentSlider.value = AudioManager.instance.environmentSource.volume;
        }

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        environmentSlider.onValueChanged.AddListener(SetEnvironmentVolume);
    }

    public void SetMusicVolume(float volume)
    {
        AudioManager.instance.SetMusicVolume(volume);
    }

    public void SetEnvironmentVolume(float volume)
    {
        AudioManager.instance.SetEnvironmentVolume(volume);
    }

    private void OnDestroy()
    {
        // prevent memory leaks
        musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        environmentSlider.onValueChanged.RemoveListener(SetEnvironmentVolume);
    }
}
