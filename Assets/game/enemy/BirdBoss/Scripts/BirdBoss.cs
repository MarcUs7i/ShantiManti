using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BirdBoss : MonoBehaviour
{
    public Transform target;

    [Header("Speed & Range")]
    public float defaultSpeed = 700f;
    public float StartRange = 20f;
    public float attackableRange = 10f;

    [Header("stage2")]
    public float stage2Speed = 1400f;
    public float stage2AnimSec = 2.0f;

    [Header("Pathfinding")]
    public float nextWaypointDistance = 3f;
    private int _currentWaypoint;

    [Header("Audio")]
    public AudioSource BackgroundMusic;
    private AudioSource _audioSfx;

    [Header("Health")]
    public int health = 100;
    public GameObject deathEffect;
    
    [Header("Animation IDs")]
    private static readonly int AttackAnimationID = Animator.StringToHash("Attack");
    private static readonly int DamageAnimationID = Animator.StringToHash("Damage");
    private static readonly int EnterStage2AnimationID = Animator.StringToHash("EnterStage2");

    private Path _path;
    private Animator _animator;
    private Transform _enemyGfx;
    private Seeker _seeker;
    private Rigidbody2D _rb;

    private bool _startGoing;
    private bool _stopHurting;
    private bool _isInStage2;
    private bool _transitioningToStage2;

    private void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _enemyGfx = GetComponentInChildren<SpriteRenderer>().transform;

        _audioSfx = GetComponent<AudioSource>();
        _startGoing = false;

        InvokeRepeating(nameof(UpdatePath), 0f, .5f);
    }

    private void UpdatePath()
    {
        if (_seeker.IsDone())
        {
            _seeker.StartPath(_rb.position, target.position, OnPathComplete);
        }
    }

    private void OnPathComplete (Path p)
    {
        if (p.error)
        {
            return;
        }
        _path = p;
        _currentWaypoint = 0;
    }

    private void FixedUpdate()
    {
        if(Pause.IsPause)
        {
            return;
        }
        
        var distance = Vector2.Distance(transform.position, target.position);

        if (distance < StartRange && !_startGoing)
        {
            _startGoing = true;
        }
        
        if (health <= 25 && !_isInStage2)
        {
            StartCoroutine(GetToStage2());
        }

        if (!_transitioningToStage2 && _startGoing)
        {
            if (_path == null || _currentWaypoint >= _path.vectorPath.Count)
            {
                return;
            }

            Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] -_rb.position).normalized;
            Vector2 force = direction * (defaultSpeed * Time.deltaTime);

            _rb.AddForce(force);

            distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                _currentWaypoint++;
            }

            // Flip enemy sprite
            if (_rb.linearVelocity.x >= 0.01f)
            {
                _enemyGfx.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (_rb.linearVelocity.x <= -0.01f)
            {
                _enemyGfx.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var distance = Vector2.Distance(transform.position, target.position);
        if (collider.gameObject.CompareTag("Bullet") && distance < attackableRange && !_stopHurting)
        {
            TakeDamage(10);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Attack());
        }
    }

    // Health
    private void TakeDamage(int damage)
    {
        if (MainMenu.ExitLevel)
        {
            return;
        }
        health -= damage;

        StartCoroutine(AnimateDamage());
        StartCoroutine(BulletAttacked());

        if (health <= 0)
        {
            // Die
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private IEnumerator Attack()
    {
        _animator.SetBool(AttackAnimationID, true);
        Enemy.TookDamage = true;
        yield return new WaitForSeconds(2.0f);
        _animator.SetBool(AttackAnimationID, false);
    }

    private IEnumerator AnimateDamage()
    {
        _animator.SetBool(DamageAnimationID, true);
        yield return new WaitForSeconds(2.0f);
        _animator.SetBool(DamageAnimationID, false);
    }

    private IEnumerator GetToStage2()
    {
        _stopHurting = true;
        _isInStage2 = true;

        _animator.SetBool(EnterStage2AnimationID, true);
        _transitioningToStage2 = true;
        StartCoroutine(PlaySfx());

        defaultSpeed = stage2Speed;
        yield return new WaitForSeconds(stage2AnimSec);

        _transitioningToStage2 = false;
        _stopHurting = false;
    }

    private IEnumerator BulletAttacked()
    {
        _stopHurting = true;
        yield return new WaitForSeconds(2.0f);
        _stopHurting = false;
    }

    private IEnumerator PlaySfx()
    {
        var oldVolume = BackgroundMusic.volume;

        BackgroundMusic.volume = 0.25f;
        _audioSfx.Play();

        yield return new WaitForSeconds(3.0f);
        BackgroundMusic.volume = oldVolume;
    }
}
