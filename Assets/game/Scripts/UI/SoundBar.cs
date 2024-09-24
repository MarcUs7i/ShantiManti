using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBar : MonoBehaviour
{
    public static float SoundVolume = 0.75f;
    public AudioSource BackgroundMusic;
    public static float MusicTime = 0f; // Static variable to store the music time
    public static bool SceneReloaded = false; // Static variable to track if scene is reloaded

    void Start()
    {
        LoadSoundVolume();
        ApplySoundVolume();

        if (SceneReloaded)
        {
            // If the scene has been reloaded before, continue playing from where it stopped
            BackgroundMusic.time = MusicTime;
        }
        SceneReloaded = false;
    }

    void Update()
    {
        if (SoundVolume != BackgroundMusic.volume)
        {
            SoundVolume = BackgroundMusic.volume;
            SaveSoundVolume();
        }

        MusicTime = BackgroundMusic.time;
    }

    public static void SaveSoundVolume()
    {
        PlayerPrefs.SetFloat("SoundVolume", SoundVolume);
        PlayerPrefs.Save();
    }

    void LoadSoundVolume()
    {
        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("SoundVolume");
            SoundVolume = savedVolume;
        }
    }

    void ApplySoundVolume()
    {
        BackgroundMusic.volume = SoundVolume;
    }
}
