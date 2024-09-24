using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;
	public AudioSource audioSource;

	public float runSpeed = 40f;

	public static float horizontalMove = 0f;
	public static bool jump = false;
	public static bool crouch = false;
	bool IsPauseing = false;
	bool IsPlay = false;
	bool moveLeft;
	bool moveRight;
	/*public static bool AllowJump = true;
	public float JumpTime = 0.35f;

	public static bool AllowJumpIndicator = false;*/

	void Start()
	{
		/*AllowJumpIndicator = false;
		AllowJump = true;*/
		crouch = false;
	}

	// Update is called once per frame
	void Update () {
		IsPlay = SC_2DCoin.IsPlaying;
		IsPauseing = Pause.IsPause;

		if (IsPlay == true)
		{
			audioSource.Play();
			SC_2DCoin.IsPlaying = false;
		}

		if (IsPauseing == false)
		{
			//Debug.Log(horizontalMove);

			//UI buttons
			if (moveLeft && Bonus.BonusForJump == false)
			{
				horizontalMove = -40f;
			}
			if (moveRight && Bonus.BonusForJump == false)
			{
				horizontalMove = 40f;
			}
			else if (!moveLeft && !moveRight)
			{
				horizontalMove = 0f;
			}
			if (horizontalMove == 0f && Bonus.BonusForJump == false)
			{
				horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
			}
	
			animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

			//If you want to use JumpWait() then add here | this code:  && AllowJump == true
														//V
			if (Input.GetButtonDown("Jump") && Bonus.BonusForJump == false)
			{
				jump = true;
				animator.SetBool("IsJumping", true);
			}

			if (Enemy.TakedDamage == true)
			{
				jump = true;
				animator.SetBool("IsJumping", true);
			}

			if (Input.GetButtonDown("Crouch") && Bonus.BonusForJump == false)
			{
				crouch = true;
			}
			else if (Input.GetButtonUp("Crouch"))
			{
				crouch = false;
			}

			/*if (AllowJumpIndicator)
			{
				StartCoroutine(JumpWait());
				AllowJumpIndicator = false;
			}*/

		}
	}

	public void OnLanding ()
	{
		animator.SetBool("IsJumping", false);
	}

	public void OnCrouching (bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching);
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}

	public void PointerDownLeft()
	{
		moveLeft = true;
	}

	public void PointerUpLeft()
	{
		moveLeft = false;
	}

	public void PointerDownRight()
	{
		moveRight = true;
	}

	public void PointerUpRight()
	{
		moveRight = false;
	}

	public void JumpButton()
	{
		/*if (AllowJump)
		{*/
			jump = true;
			animator.SetBool("IsJumping", true);
		//}
	}

	public void CrouchButtonDown()
	{
		crouch = true;
	}

	public void CrouchButtonUP()
	{
		crouch = false;
	}


	/*IEnumerator JumpWait()
	{
		AllowJump = false;
		yield return new WaitForSeconds(JumpTime);
		AllowJump = true;

	}*/
}
