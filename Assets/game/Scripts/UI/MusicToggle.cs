using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    public static bool musicToggled = true;
    public Toggle musicToggle;
    public AudioSource BackgroundMusic;

    private bool isUpdatingMusicState = false;

    void Start()
    {
        LoadMusicState();

        // Add a listener for the value changed event
        if (musicToggle != null)
        {
            musicToggle.onValueChanged.AddListener(OnMusicToggleValueChanged);
        }

        UpdateMusicState();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            MuteMusic();
        }
    }

    public void MuteMusic()
    {
        musicToggled = !musicToggled;

        SaveMusicState();
        UpdateMusicState();
    }

    // Listener for the value changed event
    void OnMusicToggleValueChanged(bool isOn)
    {
        if (!isUpdatingMusicState)
        {
            MuteMusic(); // Toggle the music state when the toggle changes
        }
    }

    void SaveMusicState()
    {
        PlayerPrefs.SetInt("MusicToggled", musicToggled ? 1 : 0);
        PlayerPrefs.Save();
    }

    void LoadMusicState()
    {
        if (PlayerPrefs.HasKey("MusicToggled"))
        {
            int musicState = PlayerPrefs.GetInt("MusicToggled");
            musicToggled = (musicState == 1);
        }

        if (musicToggle != null)
        {
            musicToggle.isOn = musicToggled;
        }
    }

    void UpdateMusicState()
    {
        isUpdatingMusicState = true;

        if (musicToggled)
        {
            BackgroundMusic.Play();
        }
        else
        {
            BackgroundMusic.Pause();
        }

        if (musicToggle != null)
        {
            musicToggle.isOn = musicToggled;
        }

        isUpdatingMusicState = false;
    }
}
