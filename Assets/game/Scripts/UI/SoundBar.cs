using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBar : MonoBehaviour
{
    public static float SoundVolume = 0.75f;
    public AudioSource BackgroundMusic;
    private static float _musicTime; // Static variable to store the music time
    public static bool SceneReloaded; // Static variable to track if scene is reloaded

    private void Start()
    {
        LoadSoundVolume();
        ApplySoundVolume();

        if (SceneReloaded)
        {
            // If the scene has been reloaded before, continue playing from where it stopped
            BackgroundMusic.time = _musicTime;
        }
        SceneReloaded = false;
    }

    private void Update()
    {
        if (!Mathf.Approximately(SoundVolume, BackgroundMusic.volume))
        {
            SoundVolume = BackgroundMusic.volume;
            SaveSoundVolume();
        }

        _musicTime = BackgroundMusic.time;
    }

    private static void SaveSoundVolume()
    {
        PlayerPrefs.SetFloat("SoundVolume", SoundVolume);
        PlayerPrefs.Save();
    }

    private static void LoadSoundVolume()
    {
        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            var savedVolume = PlayerPrefs.GetFloat("SoundVolume");
            SoundVolume = savedVolume;
        }
    }

    private void ApplySoundVolume()
    {
        BackgroundMusic.volume = SoundVolume;
    }
}
