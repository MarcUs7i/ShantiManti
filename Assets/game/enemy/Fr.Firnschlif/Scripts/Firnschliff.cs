using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Firnschliff : MonoBehaviour
{
    [Header("Speed & Range")]
    public float range = 10f;
    public float SlowSpeed = 0.5f;
    public float NormalSpeed = 3f;
    private float _speed = 3f;

    [Header("Health")]
    public int health = 100;
    public GameObject deathEffect;

    [Header("Weapon")]
    public Transform firePoint;
    public GameObject EnemyWeapon;

    [Header("Ground Check")]
    public Collider2D groundCheckCollider;
    
    [Header("Animation IDs")]
    private static readonly int AttackAnimationID = Animator.StringToHash("Attack");
    private static readonly int DamageAnimationID = Animator.StringToHash("Damage");
    
    private Animator _animator;
    private Transform _player;
    
    public static float BulletNuggetDirection;
    private bool _canDestroyItself;
    private bool _stop;
    private bool _stopHurting;


    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _speed = NormalSpeed;

        BulletNuggetDirection = -1f;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _animator.SetBool(DamageAnimationID, true);
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

    private void FixedUpdate()
    {
        if (_animator.GetBool(DamageAnimationID))
        {
            StartCoroutine(DamageAnimation());
        }

        if (Pause.IsPause)
        {
            return;
        }
        
        // calculate the distance between the enemy and the player
        var distance = Vector2.Distance(transform.position, _player.position);
        
        if (distance < range)
        {
            // move the enemy towards the player at the specified speed
            transform.position += Vector3.left * (_speed * Time.deltaTime);
            if (!_stop)
            {
                StartCoroutine(Attack());
            }

            // check if the enemy is not on the ground
            if (!IsOnGround())
            {
                DestroyEnemy();
            }
            
            _canDestroyItself = true;
        }
        
        if (distance > range && _canDestroyItself)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Attack()
    {
        _stop = true;
        yield return new WaitForSeconds(2.0f);

        _speed = SlowSpeed;
        _animator.SetBool(AttackAnimationID, true);
        Instantiate(EnemyWeapon, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(2.0f);
        _speed = NormalSpeed;
        _animator.SetBool(AttackAnimationID, false);
        
        yield return new WaitForSeconds(2.0f);
        _stop = false;
    }

    private IEnumerator DamageAnimation()
    {
        yield return new WaitForSeconds(2.0f);
        _animator.SetBool(DamageAnimationID, false);
    }

    private IEnumerator BulletAttacked()
    {
        _animator.SetBool(DamageAnimationID, true);
        _stopHurting = true;
        yield return new WaitForSeconds(0.5f);
        _stopHurting = false;
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

    void DestroyEnemy()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
