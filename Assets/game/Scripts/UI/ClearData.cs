using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearData : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject ConfirmationDialog;
    public GameObject DarkBackground;
    public GameObject ErrorDialog;

    [Header("")]
    public float waitForDeletion = 0.1f;

    void Start()
    {
        ConfirmationDialog.SetActive(true);
        DarkBackground.SetActive(false);
        ErrorDialog.SetActive(false);
    }

    public void Confirm()
    {
        ErrorDialog.SetActive(false);
        ConfirmationDialog.SetActive(false);
        DarkBackground.SetActive(true);
        
        StartCoroutine(CheckDelete());
    }

   IEnumerator CheckDelete()
    {
        PlayerSaving.DeletePlayer();
        yield return new WaitForSeconds(waitForDeletion);

        int[] defaultInts = { 0, 0 };
        bool[] defaultBools = { false, true };
        int[] checkInts = { PlayerSaving.level, PlayerSaving.coins };
        bool[] checkBools = { PlayerSaving.hasCompletedTutorial, PlayerSaving.movingClouds };

        for (int i = 0; i < defaultInts.Length; i++)
        {
            if (checkInts[i] != defaultInts[i] || checkBools[i] != defaultBools[i])
            {
                ErrorDialog.SetActive(true);
                DarkBackground.SetActive(false);
                yield break;
            }
        }

        MainMenu mainMenu = FindObjectOfType<MainMenu>().GetComponent<MainMenu>();
        mainMenu.LoadScene(0);
    }
}
