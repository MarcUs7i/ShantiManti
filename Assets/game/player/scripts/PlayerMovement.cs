using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;
	public AudioSource audioSource;

	public float runSpeed = 40f;

	public static float HorizontalMove;
	private static bool _jump;
	public static bool Crouch;
	private bool _moveLeft;
	private bool _moveRight;
	
	private static readonly int Speed = Animator.StringToHash("Speed");
	private static readonly int IsJumping = Animator.StringToHash("IsJumping");
	private static readonly int IsCrouching = Animator.StringToHash("IsCrouching");

	private void Start()
	{
		Crouch = false;
	}

	public void PlayCoinSound()
	{
		audioSource.Play();
	}

	private void Update()
	{
		if (Pause.IsPause)
		{
			return;
		}
		
		//UI buttons
		if (_moveLeft && !Bonus.BonusForJump)
		{
			HorizontalMove = -40f;
		}
		if (_moveRight && !Bonus.BonusForJump)
		{
			HorizontalMove = 40f;
		}
		else if (!_moveLeft && !_moveRight)
		{
			HorizontalMove = 0f;
		}
		if (HorizontalMove == 0f && !Bonus.BonusForJump)
		{
			HorizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		}
	
		animator.SetFloat(Speed, Mathf.Abs(HorizontalMove));

		if (Input.GetButtonDown("Jump") && !Bonus.BonusForJump)
		{
			_jump = true;
			animator.SetBool(IsJumping, true);
		}

		if (Enemy.TookDamage)
		{
			_jump = true;
			animator.SetBool(IsJumping, true);
		}

		if (Input.GetButtonDown("Crouch") && !Bonus.BonusForJump)
		{
			Crouch = true;
		}
		else if (Input.GetButtonUp("Crouch"))
		{
			Crouch = false;
		}
	}

	public void OnLanding()
	{
		animator.SetBool(IsJumping, false);
	}

	public void OnCrouching(bool isCrouching)
	{
		animator.SetBool(IsCrouching, isCrouching);
	}

	private void FixedUpdate()
	{
		// Move our character
		controller.Move(HorizontalMove * Time.fixedDeltaTime, Crouch, _jump);
		_jump = false;
	}

	public void PointerDownLeft()
	{
		_moveLeft = true;
	}

	public void PointerUpLeft()
	{
		_moveLeft = false;
	}

	public void PointerDownRight()
	{
		_moveRight = true;
	}

	public void PointerUpRight()
	{
		_moveRight = false;
	}

	public void JumpButton()
	{
		_jump = true;
		animator.SetBool(IsJumping, true);
	}

	public void CrouchButtonDown()
	{
		Crouch = true;
	}

	public void CrouchButtonUP()
	{
		Crouch = false;
	}
}
