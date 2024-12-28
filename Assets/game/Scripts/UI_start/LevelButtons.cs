using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtons : MonoBehaviour
{
    private ButtonData[] _buttonData;
    public Button[] buttons;
    private MainMenu _mainMenu;

    private int _level;

    private void Awake()
    {
        _buttonData = new ButtonData[buttons.Length];
        for (var i = 0; i < _buttonData.Length; i++)
        {
            _buttonData[i] = new ButtonData(buttons[i], i + 1);
        }
        _mainMenu = FindFirstObjectByType<MainMenu>().GetComponent<MainMenu>();

        _level = PlayerSaving.Level;

        InitializeButtonListeners();
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        for (var i = 0; i < _level; i++)
        {
            if (_buttonData.Length > i)
            {
                _buttonData[i].Button.interactable = true;
            }
        }
        for (var i = _level; i < _buttonData.Length; i++)
        {
            _buttonData[i].Button.interactable = false;
        }

        _buttonData[0].Button.interactable = true;
        _buttonData[^1].Button.interactable = true;
    }

    private void InitializeButtonListeners()
    {
        foreach (var data in _buttonData)
        {
            if (data.Button.interactable)
            {
                data.Button.onClick.AddListener(() => _mainMenu.StartLevel(data.Level));
            }
        }
    }

    // Test if its working
    /*private IEnumerator Start()
    {
        _level++;
        UpdateButtons();
        Debug.Log("Level: " + _level);
        yield return new WaitForSeconds(1f);
        _level++;
        UpdateButtons();
        Debug.Log("Level: " + _level);
        yield return new WaitForSeconds(1f);
        _level++;
        UpdateButtons();
        Debug.Log("Level: " + _level);
        yield return new WaitForSeconds(1f);
        _level++;
        UpdateButtons();
        Debug.Log("Level: " + _level);
        yield return new WaitForSeconds(1f);
        _level--;
        UpdateButtons();
        Debug.Log("Level: " + _level);
    }*/

    private class ButtonData
    {
        public readonly Button Button;
        public readonly int Level;

        public ButtonData(Button button, int level)
        {
            Button = button;
            Level = level;
        }
    }
}
