using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Justin : MonoBehaviour
{
    public float range = 10f; // the range at which the enemy will start moving towards the player
    public float speed = 5f; // the speed at which the enemy will move

    private Transform player; // reference to the player's transform
    private Rigidbody2D rb; // reference to the enemy's Rigidbody2D component
    public Collider2D groundCheckCollider;

    public GameObject deathEffect; // the death effect prefab
    public Animator playerAnimator;

    void Start()
    {
        // find the player's transform by finding the object with the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // get the enemy's Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // get the enemy's collider component
    }

    void Update()
    {
        // calculate the distance between the enemy and the player
        float distance = Vector2.Distance(transform.position, player.position);

        // if the distance between the enemy and the player is less than the range
        if (distance < range && Pause.IsPause == false)
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
                // destroy the enemy game object
                DestroyEnemy();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // The enemy has collided with the player
            if (collision.relativeVelocity.y < 0 && transform.position.y < collision.transform.position.y)
            {
                // The player is colliding with the top of the enemy from above
                // Destroy the enemy game object
                PlayerMovement.jump = true;
                playerAnimator.SetBool("IsJumping", true);
                //PlayerMovement.AllowJump = false;
                DestroyEnemy();
            }
            else
            {
                // The player is colliding with the side or bottom of the enemy, or the player is not colliding with the top of the enemy from above
                // Destroy the player game object
                Enemy.TakedDamage = true;
            }
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


    void DestroyEnemy()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
