using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Firnschliff : MonoBehaviour
{

    public Transform target;
    public Transform firePoint;
    public float speed = 3f;
    public float SlowSpeed = 0.5f;
    public float NormalSpeed = 3f;

    public float range = 10f; // the range at which the enemy will start moving towards the player
    float distance;
    public Transform enemyGFX;
    private Transform player; // reference to the player's transform
    public GameObject EnemyWeapon;

    Rigidbody2D rb;
    public Animator animator;
    bool Stop = false;
    public static float BulletNuggetDirection;

    // Health
    public int health = 100;
    public GameObject deathEffect;

    bool TimeStopHearting = false;

    public Collider2D groundCheckCollider;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        BulletNuggetDirection = 0f;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("Damage", true);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bullet" && distance < range && TimeStopHearting == false)
        {
            StartCoroutine(BulletAttacked());
            TakeDamage(50);
        }
    }


    public void TakeDamage(int damage)
    {
        if (MainMenu.ExitLevel == false)
		{
			health -= damage;

			if (health <= 0)
			{
				Die();
			}
		}
    }

    void Die ()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void Update()
    {
        if (animator.GetBool("Damage"))
        {
            StartCoroutine(DamageAnimation());
        }
        
        // calculate the distance between the enemy and the player
        distance = Vector2.Distance(transform.position, player.position);

        // if the distance between the enemy and the player is less than the range
        if (distance < range && Pause.IsPause == false)
        {
            // move the enemy towards the player at the specified speed
            /*Vector2 direction = (player.position - transform.position).normalized;
            Debug.Log(direction);
            rb.velocity = direction * speed;*/
            transform.position += Vector3.left * speed * Time.deltaTime;
            distance = Vector2.Distance(transform.position, player.position);
            if (Stop == false)
            {
                StartCoroutine(Attack());
            }
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

    IEnumerator Attack()
    {
        Stop = true;
        yield return new WaitForSeconds(2.0f);
        speed = SlowSpeed;
        animator.SetBool("Attack", true);
        Instantiate(EnemyWeapon, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(2.0f);
        speed = NormalSpeed;
        animator.SetBool("Attack", false);
        StartCoroutine(Wait());
    }

    IEnumerator DamageAnimation()
    {
        yield return new WaitForSeconds(2.0f);
        animator.SetBool("Damage", false);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.0f);
        Stop = false;
    }

    IEnumerator BulletAttacked()
    {
        animator.SetBool("Damage", true);
        TimeStopHearting = true;
        yield return new WaitForSeconds(0.5f);
        TimeStopHearting = false;
        animator.SetBool("Damage", false);
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
