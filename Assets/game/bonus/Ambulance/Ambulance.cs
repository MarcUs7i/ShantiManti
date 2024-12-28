using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ambulance : MonoBehaviour
{
    private Animator _animator;
    private Animator _gfx;
    public bool allowedToKillHauer;

    [Header("Timings")]
    public float timeToDriveToPickUpLocation = 1.0f;
    public float timeToWaitAtPickUp = 2.0f;
    public float timeToDriveToStation = 1.0f;
    public float timeout = 1.1f;
    
    [Header("AnimationIDs")]
    private static readonly int GoToPickUpLocationAnimationID = Animator.StringToHash("GoToPickUpLocation");
    private static readonly int DriveAnimationID = Animator.StringToHash("Drive");
    private static readonly int StayAtPickUpLocationAnimationID = Animator.StringToHash("StayAtPickUpLocation");
    private static readonly int GoToStationAnimationID = Animator.StringToHash("GoToStation");
    private static readonly int StayAtStationAnimationID = Animator.StringToHash("StayAtStation");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _gfx = transform.GetChild(0).GetComponent<Animator>();
    }

    public IEnumerator Script()
    {
        yield return new WaitForSeconds(timeout);

        _animator.SetBool(GoToPickUpLocationAnimationID, true);
        _gfx.SetBool(DriveAnimationID, true);

        yield return new WaitForSeconds(timeToDriveToPickUpLocation);

        _animator.SetBool(GoToPickUpLocationAnimationID, false);
        _gfx.SetBool(DriveAnimationID, false);

        _animator.SetBool(StayAtPickUpLocationAnimationID, true);

        allowedToKillHauer = true;

        yield return new WaitForSeconds(timeToWaitAtPickUp);

        _animator.SetBool(StayAtPickUpLocationAnimationID, false);
        _gfx.SetBool(DriveAnimationID, true);
        _animator.SetBool(GoToStationAnimationID, true);

        yield return new WaitForSeconds(timeToDriveToStation);

        _animator.SetBool(GoToStationAnimationID, false);
        _gfx.SetBool(DriveAnimationID, false);
        _animator.SetBool(StayAtStationAnimationID, true);
        
    }
}
