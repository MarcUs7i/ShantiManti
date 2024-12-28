using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool IsPause;
    public Animator animator;
    public GameObject HealthBar;
    public GameObject CoinCounterUI;
    public GameObject PauseButton;
    public GameObject PauseMenu;
    public GameObject Left;
    public GameObject Right;
    public GameObject Jump;
    public GameObject Crouch;
    public GameObject Fire;
    public GameObject Image;

    private static readonly int IsCrouching = Animator.StringToHash("IsCrouchingAnimationID");

    public void IsPauseing()
    {
        PauseGame();
    }

    public void IsNotPauseing()
    {
        PauseGame();
    }

    private void Start() //Set to Start() or else InputActions will be null (not initialized fast enough)
    {
        PlayerMovement.InputActions.Player.Pause.performed += ctx => PauseGame();
    }
    
    public void PauseGame()
    {
        if (Bonus.BonusForJump)
        {
            return;
        }
        
        IsPause = !IsPause;
        PauseMenu.SetActive(IsPause);
        PauseButton.SetActive(!IsPause);
        CoinCounterUI.SetActive(!IsPause);
        HealthBar.SetActive(!IsPause);
        Left.SetActive(!IsPause);
        Right.SetActive(!IsPause);
        Jump.SetActive(!IsPause);
        Crouch.SetActive(!IsPause);
        Fire.SetActive(!IsPause);
        if (Image != null && IsPause)
        {
            Image.SetActive(false);
        }
        if(!IsPause) // if going back to the game
        {
            PlayerMovement.Crouch = false;
            animator.SetBool(IsCrouching, false);
        }
    }
}
