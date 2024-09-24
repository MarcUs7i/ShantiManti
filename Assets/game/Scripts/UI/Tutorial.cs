using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Button level1;
    public static bool pressedBack = true;

    void Awake()
    {
        //level1 = GetComponent<Button>();
        level1.interactable = false;
        StartCoroutine(WaitForTutorial());
    }

    void Update()
    {
        if(!pressedBack)
        {
            StartCoroutine(WaitForTutorial());
        }
    }

    IEnumerator WaitForTutorial()
    {
        yield return new WaitForSeconds(4.0f);
        if(!pressedBack)
        {
            level1.interactable = true;
        }
    }

    public void backButton()
    {
        pressedBack = true;
    }

    public void nextButton()
    {
        pressedBack = false;
    }
}
