using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip gameMusicClip;

    void Start()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayMusic(gameMusicClip);
        }
    }

}
