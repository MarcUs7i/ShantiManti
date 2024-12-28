using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Button button;
    public GameObject[] menus;
    private MainMenu _mainMenu;
    private int _currentPage;

    private InputActions _inputActions;

    private void Awake()
    {
        _inputActions = new InputActions();
        _inputActions.Player.Start.performed += _ => NextPage();
        _inputActions.Player.Next.performed += _ => NextPage();
        _inputActions.Player.Previous.performed += _ => PreviousPage();
        
        _mainMenu = FindFirstObjectByType<MainMenu>().GetComponent<MainMenu>();
        button.interactable = false;
    }
    
    private void OnEnable() => _inputActions.Enable();
    
    private void OnDisable() => _inputActions.Disable();

    public void NextPage()
    {
        if (_currentPage < menus.Length - 1)
        {
            menus[_currentPage].SetActive(false);
            _currentPage++;
            menus[_currentPage].SetActive(true);
        }

        if (_currentPage == menus.Length - 1 && !button.interactable)
        {
            StartCoroutine(ActivateStartLevelButton());
        }
        else if (_currentPage == menus.Length - 1)
        {
            StartCoroutine(StartGame());
        }
    }

    public void PreviousPage()
    {
        if (_currentPage > 0)
        {
            menus[_currentPage].SetActive(false);
            _currentPage--;
            menus[_currentPage].SetActive(true);
        }

        if (_currentPage == menus.Length - 2)
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator StartGame()
    {
        PlayerSaving.HasCompletedTutorial = true;
        PlayerSaving.SavePlayer();
        yield return new WaitForSeconds(0.5f);
        _mainMenu.StartLevel(1);
    }

    private IEnumerator ActivateStartLevelButton()
    {
        yield return new WaitForSeconds(4.0f);
        button.interactable = true;
    }
}
