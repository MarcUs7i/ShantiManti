using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudsToggle : MonoBehaviour
{
    public static bool cloudsCanMove;
    public Toggle cloudsToggle;
    bool previousCloudsCanMoveState;
    bool changingStates = false;

    void Start()
    {
        changingStates = true;

        cloudsCanMove = PlayerSaving.movingClouds;
        cloudsToggle.isOn = cloudsCanMove;
        previousCloudsCanMoveState = cloudsCanMove;

        changingStates = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeBool();
        }

        if (cloudsCanMove != previousCloudsCanMoveState)
        {
            //Debug.Log("State changed. Saving Clouds state: " + cloudsCanMove);
            PlayerSaving.movingClouds = cloudsCanMove;
            PlayerSaving.SavePlayer();
            previousCloudsCanMoveState = cloudsCanMove;
        }
    }

    public void ChangeBool()
    {
        if (changingStates)
        {
            return;
        }
        changingStates = true;

        cloudsToggle.isOn = !cloudsCanMove;
        cloudsCanMove = !cloudsCanMove;
        
        changingStates = false;
        //Debug.Log("Clouds state changed by user: " + cloudsCanMove);
    }
}