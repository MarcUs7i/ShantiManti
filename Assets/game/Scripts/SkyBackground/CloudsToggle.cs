using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudsToggle : MonoBehaviour
{
    public static bool CloudsCanMove;
    public Toggle cloudsToggle;
    private bool _previousCloudsCanMoveState;
    private bool _changingStates;

    private static InputActions _inputActions;

    private void Awake()
    {
        _inputActions = new InputActions(); 
        _inputActions.Player.Clouds.performed += ctx => ChangeBool();
    }

    private void Start()
    {
        _changingStates = true;

        CloudsCanMove = PlayerSaving.MovingClouds;
        cloudsToggle.isOn = CloudsCanMove;
        _previousCloudsCanMoveState = CloudsCanMove;

        _changingStates = false;
    }
    
    private void OnEnable() => _inputActions.Enable();

    private void OnDisable() => _inputActions.Disable();

    private void Update()
    {
        if (CloudsCanMove == _previousCloudsCanMoveState) return;
        
        PlayerSaving.MovingClouds = CloudsCanMove;
        PlayerSaving.SavePlayer();
        _previousCloudsCanMoveState = CloudsCanMove;
    }

    public void ChangeBool()
    {
        if (_changingStates)
        {
            return;
        }
        _changingStates = true;

        cloudsToggle.isOn = !CloudsCanMove;
        CloudsCanMove = !CloudsCanMove;
        
        _changingStates = false;
    }
}