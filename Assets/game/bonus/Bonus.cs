using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    private Transform _hauer;
    public GameObject[] disableObjects;
    private Transform _player;
    private Animator _animator;
    private Animator _gfx;
    private Transform _childTransform;
    private MainMenu _mainMenu;
    private Ambulance _ambulance;
    
    public static bool BonusForJump;

    [Header("Settings")]
    public float range = 15f;

    [Header("Timings")]
    public float secondsToWalk = 3f;
    public float secondsToFall = 0.5f;
    public float timeout = 7.0f;
    
    [Header("AnimationIDs")]
    private static readonly int WalkAnimationAnimationID = Animator.StringToHash("WalkAnimation");
    private static readonly int WalkTransformAnimationID = Animator.StringToHash("WalkTransform");
    private static readonly int FallAnimationID = Animator.StringToHash("Fall");
    private static readonly int LayOnTheGroundAnimationID = Animator.StringToHash("LayOnTheGround");

    private bool _walk = true;
    private bool _isCoroutineRunning;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _hauer = this.GetComponent<Transform>();
        _childTransform = _hauer.GetChild(0);
        _mainMenu = FindFirstObjectByType<MainMenu>().GetComponent<MainMenu>();

        _animator = GetComponent<Animator>();
        _gfx = _childTransform.GetComponent<Animator>();

        _ambulance = FindFirstObjectByType<Ambulance>().GetComponent<Ambulance>();

        BonusForJump = false;
        _walk = false;
        _gfx.SetBool(WalkAnimationAnimationID, _walk);
    }

    private void Update()
    {
        var distance = Vector2.Distance(transform.position, _player.position);
        if (distance > range)
        {
            return;
        }
        
        Vector3 newPosition = new Vector3(12.60997f, -20.81544f, 0f);
        _player.transform.position = newPosition;
        foreach (var disableObject in disableObjects)
        {
            disableObject.SetActive(false);
        }
        BonusForJump = true;

        _animator.SetBool(WalkTransformAnimationID, _walk);

        if (!_isCoroutineRunning)
        {
            StartCoroutine(StartWalk());
        }
        if (_ambulance.allowedToKillHauer)
        {
            _ambulance.allowedToKillHauer = false;
            Destroy(_childTransform.gameObject);
        }
    }

    private IEnumerator StartWalk()
    {
        _isCoroutineRunning = true;
        _walk = true;
        _gfx.SetBool(WalkAnimationAnimationID, _walk);

        yield return new WaitForSeconds(secondsToWalk);

        _walk = false;
        _gfx.SetBool(WalkAnimationAnimationID, _walk);
        _animator.SetBool(FallAnimationID, true);

        yield return new WaitForSeconds(secondsToFall);

        _animator.SetBool(FallAnimationID, false);
        _animator.SetBool(LayOnTheGroundAnimationID, true);
        StartCoroutine(_ambulance.Script());

        yield return new WaitForSeconds(timeout);
        
        BonusForJump = false;
        _mainMenu.AboutScene();
        
    }
}
