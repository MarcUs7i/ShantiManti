using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelTutorial : MonoBehaviour
{
    public string level1 = "level1";

    public SceneFader sceneFader;

    public void StartLevel1()
    {
        PlayerSaving.tutorial = 1;
        PlayerSaving.SavingPlayer = true;
        sceneFader.FadeTo(level1);
		MainMenu.ExitLevel = false;
    }
}
