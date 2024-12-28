using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public AudioSource audioSource;

    public float runSpeed = 40f;
    public float sprintSpeed = 80f;
    private float _speed;

    private Vector2 _movementInputNormalized;
    private static Vector2 _movementInput;
    public static float HorizontalMove => _movementInput.x;
    private bool _jump;
    public static bool Crouch;

    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int IsCrouching = Animator.StringToHash("IsCrouching");

    public static InputActions InputActions;

    private void Awake()
    {
        _speed = runSpeed;
        InputActions = new InputActions();

        // Bind the Move action
        InputActions.Player.Move.performed += ctx =>
        {
            _movementInputNormalized = ctx.ReadValue<Vector2>();
            _movementInput = _movementInputNormalized * _speed;
        };
        InputActions.Player.Move.canceled += ctx =>
        {
            _movementInputNormalized = Vector2.zero;
            _movementInput = Vector2.zero;
        };

        // Bind the Jump action
        InputActions.Player.Jump.performed += ctx => _jump = true;

        // Bind the Crouch action
        InputActions.Player.Crouch.performed += ctx => Crouch = true;
        InputActions.Player.Crouch.canceled += ctx => Crouch = false;
        
        // Bind the Sprint action
        InputActions.Player.Sprint.performed += ctx =>
        {
            _speed = sprintSpeed;
            _movementInput.x = _movementInputNormalized.x * _speed;
        };
        InputActions.Player.Sprint.canceled += ctx =>
        {
            _speed = runSpeed; 
            _movementInput.x = _movementInputNormalized.x * _speed;
        };
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }

    public void PlayCoinSound()
    {
        audioSource.Play();
    }

    private void Update()
    {
        if (Pause.IsPause)
        {
            _movementInputNormalized = Vector2.zero;
            _movementInput = Vector2.zero;
            _jump = false;
            animator.SetBool(IsJumping, false);
            animator.SetFloat(Speed, 0f);
            return;
        }

        animator.SetFloat(Speed, Mathf.Abs(_movementInput.x));

        if (_jump)
        {
            animator.SetBool(IsJumping, true);
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
        controller.Move(_movementInput.x * Time.fixedDeltaTime, Crouch, _jump);
        _jump = false;
    }

    // UI Button Methods
    public void PointerDownLeft()
    {
        _movementInput.x = -_speed;
    }

    public void PointerUpLeft()
    {
        _movementInput.x = 0;
    }

    public void PointerDownRight()
    {
        _movementInput.x = _speed;
    }

    public void PointerUpRight()
    {
        _movementInput.x = 0;
    }

    public void JumpButton()
    {
        _jump = true;
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
