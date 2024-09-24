using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtons : MonoBehaviour
{
    public Button level1;
    public Button level2;
    public Button level3;
    public Button level4;
    public Button level5;
    public Button level6;
    public Button level7;
    public Button level8;
    public Button level9;
    public Button level10;
    public Button level11;
    public Button level12;
    public Button level13;
    public Button level14;
    public Button level15;
    public Button level16;
    public Button level17;
    public Button level18;
    public Button level19;
    public Button level20;
    public Button bonus;
    public Button about;

    public int test = 0;
    
    void Awake()
    {
        level1.interactable = true;
        PlayerSaving.LoadingPlayer = true;
        test = PlayerSaving.levels;

        level1.interactable = true;
        level2.interactable = false;
        level3.interactable = false;
        level4.interactable = false;
        level5.interactable = false;
        level6.interactable = false;
        level7.interactable = false;
        level8.interactable = false;
        level9.interactable = false;
        level10.interactable = false;
        level11.interactable = false;
        level12.interactable = false;
        level13.interactable = false;
        level14.interactable = false;
        level15.interactable = false;
        level16.interactable = false;
        level17.interactable = false;
        level18.interactable = false;
        level19.interactable = false;
        level20.interactable = false;
        bonus.interactable = false;
        about.interactable = true;
    }

    void Update()
    {
        // Test if its working
        /*if (Input.GetKeyDown(KeyCode.U))
		{
			test--;
		}
        if (Input.GetKeyDown(KeyCode.I))
		{
			test++;
		}
        Debug.Log("Test: " + test);*/

        level1.interactable = true;
        about.interactable = true;

        if (test >= 1)
        {
            level2.interactable = true;
        }

        if (test >= 2)
        {
            level3.interactable = true;
        }

        if (test >= 3)
        {
            level4.interactable = true;
        }

        if (test >= 4)
        {
            level5.interactable = true;
        }

        if (test >= 5)
        {
            level6.interactable = true;
        }

        if (test >= 6)
        {
            level7.interactable = true;
        }

        if (test >= 7)
        {
            level8.interactable = true;
        }

        if (test >= 8)
        {
            level9.interactable = true;
        }

        if (test >= 9)
        {
            level10.interactable = true;
        }

        if (test >= 10)
        {
            level11.interactable = true;
        }

        if (test >= 11)
        {
            level12.interactable = true;
        }

        if (test >= 12)
        {
            level13.interactable = true;
        }

        if (test >= 13)
        {
            level14.interactable = true;
        }

        if (test >= 14)
        {
            level15.interactable = true;
        }

        if (test >= 15)
        {
            level16.interactable = true;
        }

        if (test >= 16)
        {
            level17.interactable = true;
        }

        if (test >= 17)
        {
            level18.interactable = true;
        }

        if (test >= 18)
        {
            level19.interactable = true;
        }

        if (test >= 19)
        {
            level20.interactable = true;
        }

        if (test >= 20)
        {
            bonus.interactable = true;
        }

        if (test >= 21)
        {
            about.interactable = true;
        }
    }
}
