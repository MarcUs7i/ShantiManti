using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Justin : MonoBehaviour
{

    [Header("Speed & Range")]
    public float range = 10f;
    public float speed = 5f;

    [Header("Colliders")]
    public Collider2D groundCheckCollider;
    public Collider2D collisionWithPlayerCollider; // USE TOP COLLIDER!!! (For some reason it works better this way)

    [Header("Death Effect Prefab")]
    public GameObject deathEffect;
    
    private Transform _player;
    private bool _canDestroyItself;

    private void Start()
    {
        // find the player's transform by finding the object with the "Player" tag
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if(Pause.IsPause)
        {
            return;
        }
        
        // calculate the distance between the enemy and the player
        var distance = Vector2.Distance(transform.position, _player.position);
        if (distance < range)
        {
            // move the enemy towards the player at the specified speed
            transform.position += Vector3.left * (speed * Time.deltaTime);

            // check if the enemy is not on the ground
            if (!IsOnGround())
            {
                DestroyEnemy();
            }

            if (CollidesWithPlayer())
            {
                Enemy.TookDamage = true;
                DestroyEnemy();
            }
            
            _canDestroyItself = true;
        }
        
        if (distance > range && _canDestroyItself)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //If player jumps on top
            DestroyEnemy();
        }
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

    /// <summary>
    /// Check if the enemy upper collider collides with the player
    /// (For some reason it ignores the upper one and only checks the others)
    /// </summary>
    /// <returns>True if the enemy collides with the player</returns>
    private bool CollidesWithPlayer()
    {
        // perform an overlap check with the isJumpedOn collider
        Collider2D[] colliders = Physics2D.OverlapBoxAll(collisionWithPlayerCollider.bounds.center, collisionWithPlayerCollider.bounds.size, 0f);
        
        // iterate through the colliders and check if any of them are considered ground
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player"))
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
