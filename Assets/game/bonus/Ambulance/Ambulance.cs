using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ambulance : MonoBehaviour
{
    private Animator animator;
    private Animator GFX;
    public bool allowedToKillHauer = false;

    [Header("Timings")]
    public float timeToDriveToPickUpLocation = 1.0f;
    public float timeToWaitAtPickUp = 2.0f;
    public float timeToDriveToStation = 1.0f;
    public float timeout = 1.1f;

    void Start()
    {
        animator = GetComponent<Animator>();
        GFX = transform.GetChild(0).GetComponent<Animator>();
    }

    public IEnumerator Script()
    {
        yield return new WaitForSeconds(timeout);

        animator.SetBool("GoToPickUpLocation", true);
        GFX.SetBool("Drive", true);

        yield return new WaitForSeconds(timeToDriveToPickUpLocation);

        animator.SetBool("GoToPickUpLocation", false);
        GFX.SetBool("Drive", false);

        animator.SetBool("StayAtPickUpLocation", true);

        allowedToKillHauer = true;

        yield return new WaitForSeconds(timeToWaitAtPickUp);

        animator.SetBool("StayAtPickUpLocation", false);
        GFX.SetBool("Drive", true);
        animator.SetBool("GoToStation", true);

        yield return new WaitForSeconds(timeToDriveToStation);

        animator.SetBool("GoToStation", false);
        GFX.SetBool("Drive", false);
        animator.SetBool("StayAtStation", true);
        
    }
}
