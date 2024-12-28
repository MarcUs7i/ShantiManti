using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Ali : MonoBehaviour
{
    public Transform firePoint;

    [Header("Speed")]
    public float NormalSpeed = 500f;
    public float FastSpeed = 700f;
    private float _speed = 500f;

    [Header("Range")]
    public float range = 10f;
    public float attackRange = 8f;
    public float SwordAttackRange = 2f;

    [Header("Stage2")]
    public float stage2Speed = 900f;
    public float stage2AnimSec = 2.0f;

    [Header("Pathfinding")]
    public float nextWaypointDistance = 3f;
    private int _currentWaypoint;

    [Header("Audio")]
    public AudioSource BackgroundMusic;
    private AudioSource _audioSfx;

    [Header("Health")]
    public int health = 200;
    private bool _stopHurting;

    [Header("GameObjects")]
    public GameObject deathEffect;
    public GameObject EnemyWeapon;
    
    [Header("Animation IDs")]
    private static readonly int AttackAnimationID = Animator.StringToHash("Attack");
    private static readonly int SwordAttackAnimationID = Animator.StringToHash("SwordAttack");
    private static readonly int DamageAnimationID = Animator.StringToHash("Damage");
    private static readonly int EnterStage2AnimationID = Animator.StringToHash("EnterStage2");
    
    private Path _path;
    private Seeker _seeker;
    private Transform _player;
    private Rigidbody2D _rb;
    private Animator _animator;
    private Transform _enemyGfx;

    public static float BulletAliDirection;
    private bool _startGoing;
    private bool _stopAttack;
    private bool _attacking;
    private bool _isInStage2;
    private bool _transitioningToStage2;

    private void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _audioSfx = GetComponent<AudioSource>();
        _enemyGfx = GetComponentInChildren<SpriteRenderer>().transform;
        _animator = _enemyGfx.GetComponent<Animator>();

        _speed = NormalSpeed;
        _startGoing = false;

        InvokeRepeating(nameof(UpdatePath), 0f, .5f);
    }

    private void UpdatePath()
    {
        if (_seeker.IsDone())
        {
            _seeker.StartPath(_rb.position, _player.position, OnPathComplete);
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
        if (Pause.IsPause)
        {
            return;
        }

        var distance = Vector2.Distance(transform.position, _player.position);
        if (distance < range && !_startGoing)
        {
            _startGoing = true;
        }
        
        if (_startGoing && !_attacking && !_transitioningToStage2)
        {
            if (_path == null || _currentWaypoint >= _path.vectorPath.Count)
            {
                return;
            }

            Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] -_rb.position).normalized;
            Vector2 force = direction * (_speed * Time.deltaTime);

            _rb.AddForce(force);

            distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                _currentWaypoint++;
            }

            // Flip the enemy sprite
            if (_rb.linearVelocity.x >= 0.01f)
            {
                _enemyGfx.transform.localScale = new Vector3(-1f, 1f, 1f);
                BulletAliDirection = 1f;
            }
            else if (_rb.linearVelocity.x <= -0.01f)
            {
                _enemyGfx.transform.localScale = new Vector3(1f, 1f, 1f);
                BulletAliDirection = -1f;
            }

            var attackDec = Mathf.Round(UnityEngine.Random.Range(0.0f, 1.0f));

            distance = Vector2.Distance(transform.position, _player.position);
            if (distance < attackRange && attackDec == 0f)
            {
                if (!_stopAttack)
                {
                    StartCoroutine(Attack());
                }
            }
            if (distance < SwordAttackRange && Mathf.Approximately(attackDec, 1f))
            {
                if (!_stopAttack)
                {
                    StartCoroutine(SwordAttack());
                }
            }
        }

        distance = Vector2.Distance(transform.position, _player.position);
        if (distance > range)
        {
            _speed = FastSpeed;
        }
        
        if (health <= 60 && !_isInStage2)
        {
            StartCoroutine(GetToStage2());
        }    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(CollisionAttack());
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        float distance = Vector2.Distance(transform.position, _player.position);
        if (collider.gameObject.CompareTag("Bullet") && distance < range && !_stopHurting)
        {
            TakeDamage(10);
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

        //only animate damage if health is above 60 (not stage2)
        if (health > 60)
        {
            StartCoroutine(AnimateDamage());
        }

        StartCoroutine(BulletAttacked());

        if (health <= 0)
        {
            //Die
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private IEnumerator Attack()
    {
        _stopAttack = true;
        yield return new WaitForSeconds(2.0f);

        _attacking = true;
        _animator.SetBool(AttackAnimationID, true);
        Instantiate(EnemyWeapon, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(2.0f);

        _animator.SetBool(AttackAnimationID, false);
        _attacking = false;
        
        yield return new WaitForSeconds(2.0f);
        _stopAttack = false;
    }

    private IEnumerator SwordAttack()
    {
        _stopAttack = true;

        yield return new WaitForSeconds(1.9f);
        _animator.SetBool(SwordAttackAnimationID, true);
        yield return new WaitForSeconds(0.1f);

        _attacking = true;
        Enemy.TookDamage = true;

        yield return new WaitForSeconds(2.0f);
        _animator.SetBool(SwordAttackAnimationID, false);
        
        _attacking = false;
        
        yield return new WaitForSeconds(2.0f);
        _stopAttack = false;
    }

    private IEnumerator CollisionAttack()
    {
        _animator.SetBool(SwordAttackAnimationID, true);
        Enemy.TookDamage = true;
        yield return new WaitForSeconds(2.0f);
        _animator.SetBool(SwordAttackAnimationID, false);
    }

    private IEnumerator AnimateDamage()
    {
        _animator.SetBool(DamageAnimationID, true);
        yield return new WaitForSeconds(2.0f);
        _animator.SetBool(DamageAnimationID, false);
    }

    private IEnumerator GetToStage2()
    {
        _isInStage2 = true;

        _transitioningToStage2 = true;
        _stopHurting = true;
        _animator.SetBool(EnterStage2AnimationID, true);

        StartCoroutine(PlaySfx());
        NormalSpeed = FastSpeed;
        FastSpeed = stage2Speed;

        yield return new WaitForSeconds(stage2AnimSec);
        _animator.SetBool(EnterStage2AnimationID, false);

        _transitioningToStage2 = false;
        _stopHurting = false;
    }

    private IEnumerator BulletAttacked()
    {
        _stopHurting = true;
        yield return new WaitForSeconds(1.5f);
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
