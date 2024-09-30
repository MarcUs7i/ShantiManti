using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Button button;
    public GameObject[] menus;
    MainMenu mainMenu;
    int currentPage = 0;

    void Awake()
    {
        mainMenu = FindObjectOfType<MainMenu>().GetComponent<MainMenu>();
        button.interactable = false;
        button.onClick.AddListener(() => StartCoroutine(StartGame()));
    }

    public void NextPage()
    {
        if (currentPage < menus.Length - 1)
        {
            menus[currentPage].SetActive(false);
            currentPage++;
            menus[currentPage].SetActive(true);
        }

        if (currentPage == menus.Length - 1)
        {
            StartCoroutine(ActivateStartLevelButton());
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            menus[currentPage].SetActive(false);
            currentPage--;
            menus[currentPage].SetActive(true);
        }

        if (currentPage == menus.Length - 2)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator StartGame()
    {
        PlayerSaving.hasCompletedTutorial = true;
        PlayerSaving.SavePlayer();
        yield return new WaitForSeconds(0.5f);
        mainMenu.StartLevel(1);
    }

    IEnumerator ActivateStartLevelButton()
    {
        yield return new WaitForSeconds(4.0f);
        button.interactable = true;
    }
}
