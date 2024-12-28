using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtons : MonoBehaviour
{
    public ButtonData[] buttonData;
    public Button[] buttons;
    private MainMenu mainMenu;

    private int level = 0;

    void Awake()
    {
        buttonData = new ButtonData[buttons.Length];
        for (int i = 0; i < buttonData.Length; i++)
        {
            buttonData[i] = new ButtonData(buttons[i], i + 1);
        }
        mainMenu = FindObjectOfType<MainMenu>().GetComponent<MainMenu>();

        level = PlayerSaving.Level;

        InitializeButtonListeners();
        UpdateButtons();
    }

    void UpdateButtons()
    {
        for (int i = 0; i < level; i++)
        {
            if (buttonData.Length > i)
            {
                buttonData[i].button.interactable = true;
            }
        }
        for (int i = level; i < buttonData.Length; i++)
        {
            buttonData[i].button.interactable = false;
        }

        buttonData[0].button.interactable = true;
        buttonData[^1].button.interactable = true;
    }

    void InitializeButtonListeners()
    {
        foreach (ButtonData data in buttonData)
        {
            if (data.button.interactable)
            {
                data.button.onClick.AddListener(() => mainMenu.StartLevel(data.level));
            }
        }
    }

    // Test if its working
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            level--;
            UpdateButtons();
            Debug.Log("Test: " + level);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            level++;
            UpdateButtons();
            Debug.Log("Test: " + level);
        }
    }*/

    public class ButtonData
    {
        public Button button;
        public int level;

        public ButtonData(Button button, int level)
        {
            this.button = button;
            this.level = level;
        }
    }
}
