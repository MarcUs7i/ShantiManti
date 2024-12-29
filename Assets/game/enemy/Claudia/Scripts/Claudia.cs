using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Claudia : MonoBehaviour
{
    [Header("Speed & Range")]
    public float speed = 200f;
    public float range = 10f;

    [Header("Pathfinding")]
    public float nextWaypointDistance = 3f;

    private Path _path;
    private Seeker _seeker;
    private Transform _player;
    private Rigidbody2D _rb;
    private Transform _enemyGfx;
    private Animator _animator;

    [Header("Health")]
    public int health = 100;
    public GameObject deathEffect;
    
    [Header("Ground Check")]
    public Collider2D groundCheckCollider;
    
    [Header("Animation IDs")]
    private static readonly int AttackAnimationID = Animator.StringToHash("Attack");
    private static readonly int DamageAnimationID = Animator.StringToHash("Damage");

    private bool _stopHurting;
    private bool _stopAttack;
    private bool _stop;
    private bool _startGoing;
    private int _currentWaypoint;

    public Claudia(Path path)
    {
        _path = path;
    }

    private void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _enemyGfx = GetComponentInChildren<SpriteRenderer>().transform;

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
        
        if (_startGoing && !_stop && !_stopAttack)
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
                _enemyGfx.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (_rb.linearVelocity.x <= -0.01f)
            {
                _enemyGfx.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        // check if the enemy is not on the ground
        if (!IsOnGround())
        {
            DestroyEnemy();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_stopAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var distance = Vector2.Distance(transform.position, _player.position);
        if (collider.gameObject.CompareTag("Bullet") && distance < range && !_stopHurting)
        {
            StartCoroutine(BulletAttacked());
            TakeDamage(50);
        }
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

    private IEnumerator Attack()
    {
        _stopAttack = true;

        _animator.SetBool(AttackAnimationID, true);
        Enemy.TookDamage = true;
        yield return new WaitForSeconds(2.0f);
        _animator.SetBool(AttackAnimationID, false);

        _stopAttack = false;
    }

    private IEnumerator BulletAttacked()
    {
        _stop = true;
        _animator.SetBool(DamageAnimationID, true);
        _stopHurting = true;
        yield return new WaitForSeconds(2.0f);
        _stopHurting = false;
        _animator.SetBool(DamageAnimationID, false);
        _stop = false;
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
