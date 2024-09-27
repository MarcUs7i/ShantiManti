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
        PauseGame();
    }

    public void IsNotPauseing()
    {
        PauseGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    public void PauseGame()
    {
        if (!Bonus.BonusForJump)
        {
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
            if (Image != null && !IsPause)
            {
                Image.SetActive(false);
            }
            if (IsPause)
            {
                PlayerMovement.horizontalMove = 0f;
                animator.SetFloat("Speed", 0f);
            }
            else
            {
                PlayerMovement.crouch = false;
                animator.SetBool("IsCrouching", false);
            }
        }
    }
}
