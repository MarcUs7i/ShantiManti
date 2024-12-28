using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    private static bool _musicToggled = true;
    public Toggle musicToggle;
    public AudioSource BackgroundMusic;

    private bool _isUpdatingMusicState;
    private static InputActions _inputActions;
    

    private void Awake()
    {
        _inputActions = new InputActions();
        _inputActions.Player.MuteMusic.performed += ctx => MuteMusic();
        LoadMusicState();

        // Add a listener for the value changed event
        if (musicToggle != null)
        {
            musicToggle.onValueChanged.AddListener(OnMusicToggleValueChanged);
        }

        UpdateMusicState();
    }
    
    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    private void MuteMusic()
    {
        _musicToggled = !_musicToggled;

        SaveMusicState();
        UpdateMusicState();
    }

    // Listener for the value changed event
    private void OnMusicToggleValueChanged(bool isOn)
    {
        if (!_isUpdatingMusicState)
        {
            MuteMusic(); // Toggle the music state when the toggle changes
        }
    }

    private static void SaveMusicState()
    {
        PlayerPrefs.SetInt("MusicToggled", _musicToggled ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadMusicState()
    {
        if (PlayerPrefs.HasKey("MusicToggled"))
        {
            int musicState = PlayerPrefs.GetInt("MusicToggled");
            _musicToggled = (musicState == 1);
        }

        if (musicToggle != null)
        {
            musicToggle.isOn = _musicToggled;
        }
    }

    private void UpdateMusicState()
    {
        _isUpdatingMusicState = true;

        if (_musicToggled)
        {
            BackgroundMusic.Play();
        }
        else
        {
            BackgroundMusic.Pause();
        }

        if (musicToggle)
        {
            musicToggle.isOn = _musicToggled;
        }

        _isUpdatingMusicState = false;
    }
}
