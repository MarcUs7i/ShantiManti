using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudsToggle : MonoBehaviour
{
    public static int cloudsMoveInt;
    public static bool cloudsCanMove = true;
    public Toggle cloudsToggle;
    //public GameObject imageOfToggleOn;

    int checkInStorage = 0;
    bool previousCloudsCanMoveState;
    bool loaded = false;

    void Start()
    {
        PlayerSaving.LoadingPlayer = true;
        checkInStorage = 0;
        StartCoroutine(waitToLoadData());
    }

    // Update is called once per frame
    void Update()
    {
        if(checkInStorage == 0)
        {
            checkInStorage++;
            cloudsMoveInt = PlayerSaving.cloudMove;
            if(cloudsMoveInt == 0)
            {
                cloudsToggle.isOn = true;
                //imageOfToggleOn.SetActive(true);
                cloudsCanMove = true;
            }
            if(cloudsMoveInt == 1)
            {
                //imageOfToggleOn.SetActive(false);
                cloudsToggle.isOn = false;
                cloudsCanMove = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeBool();
        }

        /*if ((PlayerSaving.cloudMove != 0 && cloudsCanMove) && checkInStorage != 1)
        {
            PlayerSaving.cloudMove = 0;
            PlayerSaving.SavingPlayer = true;
        }
        else if ((PlayerSaving.cloudMove != 1 && !cloudsCanMove) && checkInStorage != 1)
        {
            PlayerSaving.cloudMove = 1;
            PlayerSaving.SavingPlayer = true;
        }*/

        if (cloudsCanMove != previousCloudsCanMoveState && loaded)
        {
            //Debug.Log("State changed. Saving Clouds state: " + cloudsCanMove);
            PlayerSaving.cloudMove = cloudsCanMove ? 0 : 1;
            PlayerSaving.SavingPlayer = true;
            previousCloudsCanMoveState = cloudsCanMove;
        }

        checkInStorage++;
    }

    public void ChangeBool()
    {
        if(cloudsCanMove)
        {
            cloudsToggle.isOn = false;
            //imageOfToggleOn.SetActive(false);
            cloudsCanMove = false;
        }
        else if(!cloudsCanMove)
        {
            cloudsToggle.isOn = true;
            //imageOfToggleOn.SetActive(true);
            cloudsCanMove = true;
        }
        //Debug.Log("Clouds state changed by user: " + cloudsCanMove);
    }

    IEnumerator waitToLoadData()
    {
        yield return new WaitForSeconds(0.1f);

        cloudsMoveInt = PlayerSaving.cloudMove;
        /*if(cloudsMoveInt == 0)
        {
            cloudsToggle.isOn = true;
            //imageOfToggleOn.SetActive(true);
            cloudsCanMove = true;
        }
        if(cloudsMoveInt == 1)
        {
            //imageOfToggleOn.SetActive(false);
            cloudsToggle.isOn = false;
            cloudsCanMove = false;
        }*/
        cloudsToggle.isOn = (cloudsMoveInt == 0);
        cloudsCanMove = (cloudsMoveInt == 0);
        previousCloudsCanMoveState = cloudsCanMove;
        //Debug.Log("Clouds state changed by user: " + cloudsCanMove);
        loaded = true;
    }
}
