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

    private void Start()
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

    private IEnumerator CheckDelete()
    {
        PlayerSaving.DeletePlayer();
        yield return new WaitForSeconds(waitForDeletion);

        int[] defaultInts = { 0, 0 };
        bool[] defaultBools = { false, true };
        int[] checkInts = { PlayerSaving.Level, PlayerSaving.Coins };
        bool[] checkBools = { PlayerSaving.HasCompletedTutorial, PlayerSaving.MovingClouds };

        for (var i = 0; i < defaultInts.Length; i++)
        {
            if (checkInts[i] != defaultInts[i] || checkBools[i] != defaultBools[i])
            {
                ErrorDialog.SetActive(true);
                DarkBackground.SetActive(false);
                yield break;
            }
        }

        var mainMenu = FindFirstObjectByType<MainMenu>().GetComponent<MainMenu>();
        mainMenu.LoadScene(0);
    }
}
