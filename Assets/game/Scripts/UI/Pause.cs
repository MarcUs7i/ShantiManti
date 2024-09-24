using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool IsPause = false;
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


    public void IsPauseing()
    {
        /*IsPause = true;
        PlayerMovement.horizontalMove = 0f;
        animator.SetFloat("Speed", 0f);*/
        PauseGame();
    }

    public void IsNotPauseing()
    {
        /*IsPause = false;
        PlayerMovement.crouch = false;
        animator.SetBool("IsCrouching", false);*/
        PauseGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }
    public void PauseGame()
    {
        if(IsPause && !Bonus.BonusForJump)
        {
            IsPause = false;
            PlayerMovement.crouch = false;
            animator.SetBool("IsCrouching", false);
            PauseMenu.SetActive(false);
            PauseButton.SetActive(true);
            CoinCounterUI.SetActive(true);
            HealthBar.SetActive(true);
            Left.SetActive(true);
            Right.SetActive(true);
            Jump.SetActive(true);
            Crouch.SetActive(true);
            Fire.SetActive(true);
        }
        else if (!IsPause && !Bonus.BonusForJump)
        {
            IsPause = true;
            PlayerMovement.horizontalMove = 0f;
            animator.SetFloat("Speed", 0f);
            PauseMenu.SetActive(true);
            PauseButton.SetActive(false);
            CoinCounterUI.SetActive(false);
            HealthBar.SetActive(false);
            Left.SetActive(false);
            Right.SetActive(false);
            Jump.SetActive(false);
            Crouch.SetActive(false);
            Fire.SetActive(false);
            if (Image != null)
            {
                Image.SetActive(false);
            }
        }
    }
}
