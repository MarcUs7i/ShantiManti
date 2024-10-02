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
	bool moveLeft;
	bool moveRight;

	void Start()
	{
		crouch = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (SC_2DCoin.playCoinSound == true)
		{
			audioSource.Play();
			SC_2DCoin.playCoinSound = false;
		}

		if (!Pause.IsPause)
		{
			//UI buttons
			if (moveLeft && !Bonus.BonusForJump)
			{
				horizontalMove = -40f;
			}
			if (moveRight && !Bonus.BonusForJump)
			{
				horizontalMove = 40f;
			}
			else if (!moveLeft && !moveRight)
			{
				horizontalMove = 0f;
			}
			if (horizontalMove == 0f && !Bonus.BonusForJump)
			{
				horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
			}
	
			animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

			if (Input.GetButtonDown("Jump") && !Bonus.BonusForJump)
			{
				jump = true;
				animator.SetBool("IsJumping", true);
			}

			if (Enemy.TookDamage)
			{
				jump = true;
				animator.SetBool("IsJumping", true);
			}

			if (Input.GetButtonDown("Crouch") && !Bonus.BonusForJump)
			{
				crouch = true;
			}
			else if (Input.GetButtonUp("Crouch"))
			{
				crouch = false;
			}

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
}
