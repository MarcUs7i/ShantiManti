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
    
    private Animator playerAnimator;
    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        // find the player's transform by finding the object with the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerAnimator = player.GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // calculate the distance between the enemy and the player
        float distance = Vector2.Distance(transform.position, player.position);

        // if the distance between the enemy and the player is less than the range
        if (distance < range && !Pause.IsPause)
        {
            // move the enemy towards the player at the specified speed
            transform.position += Vector3.left * speed * Time.deltaTime;
            distance = Vector2.Distance(transform.position, player.position);
            if (distance > range)
            {
                Destroy(gameObject);
            }

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
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //If player jumps on top
            PlayerMovement.jump = true;
            playerAnimator.SetBool("IsJumping", true);
            DestroyEnemy();
        }
    }

    bool IsOnGround()
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

    bool CollidesWithPlayer()
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


    void DestroyEnemy()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
