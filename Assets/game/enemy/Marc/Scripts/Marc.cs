using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Marc : MonoBehaviour
{
    [Header("Speed & Range")]
    public float speed = 500f;
    public float range = 15f;
    public float attackRange = 8f;

    [Header("Pathfinding")]
    public float nextWaypointDistance = 3f;
    private int _currentWaypoint;

    [Header("Health")]
    public int health = 100;
    public GameObject deathEffect;

    [Header("Weapon")]
    public Transform firePoint;
    public GameObject EnemyWeapon;

    [Header("")]
    public Collider2D groundCheckCollider;
    public Transform enemyGFX;
    
    [Header("AnimationIDs")]
    private static readonly int DamageAnimationID = Animator.StringToHash("Damage");
    private static readonly int AttackAnimationID = Animator.StringToHash("Attack");

    private Path _path;
    private Seeker _seeker;
    private Rigidbody2D _rb;
    private Transform _player;
    private Animator _animator;

    public static float BulletConDirection;
    private bool _stopAttack;
    private bool _stopHurting;
    private bool _canDestroyItself;
    private bool _walkStop;

    private void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;

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

    private void TakeDamage(int damage)
    {
        if (MainMenu.ExitLevel)
        {
            return;
        }
        health -= damage;

        if (health <= 0)
        {
            DestroyEnemy();
        }
    }

    private void FixedUpdate()
    {
        if (Pause.IsPause)
        {
            return;
        }

        var distance = Vector2.Distance(transform.position, _player.position);
        if (distance < range && !_walkStop)
        {
            if (_path == null || _currentWaypoint >= _path.vectorPath.Count)
            {
                return;
            }

            Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] -_rb.position).normalized;
            Vector2 force = direction * (speed * Time.deltaTime);

            _rb.AddForce(force);

            distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                _currentWaypoint++;
            }
            //You can make look it differently, if you delete 'rb.velocity' and add 'force' instead.
            if (_rb.linearVelocity.x >= 0.01f)
            {
                enemyGFX.transform.localScale = new Vector3(-1f, 1f, 1f);
                BulletConDirection = 1f;
            }
            else if (_rb.linearVelocity.x <= -0.01f)
            {
                enemyGFX.transform.localScale = new Vector3(1f, 1f, 1f);
                BulletConDirection = -1f;
            }

            if (distance < attackRange)
            {
                if (!_stopAttack)
                {
                    StartCoroutine(Attack());
                }
            }

            _canDestroyItself = true;
        }

        if (!IsOnGround())
        {
            // destroy the enemy game object
            DestroyEnemy();
        }

        // destroy the enemy game object if it is out of range
        distance = Vector2.Distance(transform.position, _player.position);
        if (distance > range && _canDestroyItself)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _animator.SetBool(DamageAnimationID, true);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var distance = Vector2.Distance(transform.position, _player.position);
        if (collider.gameObject.CompareTag("Bullet") && distance < range && !_stopHurting)
        {
            StartCoroutine(BulletAttacked());
            TakeDamage(40);
        }
    }

    private IEnumerator Attack()
    {
        _stopAttack = true;
        yield return new WaitForSeconds(2.0f);
        _walkStop = true;
        _animator.SetBool(AttackAnimationID, true);
        Instantiate(EnemyWeapon, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(2.0f);
        _walkStop = false;
        _animator.SetBool(AttackAnimationID, false);
        
        yield return new WaitForSeconds(2.0f);
        _stopAttack = false;
    }

    IEnumerator BulletAttacked()
    {
        _walkStop = true;
        _animator.SetBool(DamageAnimationID, true);
        _stopHurting = true;
        yield return new WaitForSeconds(2.0f);
        _stopHurting = false;
        _walkStop = false;
        _animator.SetBool(DamageAnimationID, false);
    }

    private bool IsOnGround()
    {
        // perform an overlap check with the ground check collider
        Collider2D[] colliders = Physics2D.OverlapBoxAll(groundCheckCollider.bounds.center, groundCheckCollider.bounds.size, 0f);
        
        // iterate through the colliders and check if any of them are considered ground
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Ground"))
            {
                return true;
            }
        }
        
        return false;
    }


    private void DestroyEnemy()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
