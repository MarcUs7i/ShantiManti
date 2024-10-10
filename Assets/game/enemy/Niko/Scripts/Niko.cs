using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Niko : MonoBehaviour
{
    [Header("Speed & Range")]
    public float SlowSpeed = 0.5f;
    public float NormalSpeed = 3f;
    float speed = 3f;
    public float range = 10f;

    [Header("Weapon")]
    public Transform firePoint;
    public GameObject EnemyWeapon;

    [Header("Health")]
    public int health = 100;
    public GameObject deathEffect;

    [Header("")]
    public Collider2D groundCheckCollider;
    public Transform enemyGFX;

    //Rigidbody2D rb;
    Transform player;
    Animator animator;

    public static float BulletHamDirection;
    bool StopAttack = false;
    bool StopHurting = false;

    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        BulletHamDirection = -1f;
        speed = NormalSpeed;
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
        float distance = Vector2.Distance(transform.position, player.position);
        if (collider.gameObject.tag == "Bullet" && distance < range && !StopHurting)
        {
            StartCoroutine(BulletAttacked());
            TakeDamage(50);
        }
    }


    public void TakeDamage(int damage)
    {
        if (!MainMenu.ExitLevel)
		{
			health -= damage;

			if (health <= 0)
			{
				DestroyEnemy();
			}
		}
    }

    void Update()
    {
        if (animator.GetBool("Damage"))
        {
            StartCoroutine(DamageAnimation());
        }

        if (Pause.IsPause)
        {
            return;
        }
        
        // calculate the distance between the enemy and the player
        float distance = Vector2.Distance(transform.position, player.position);

        // if the distance between the enemy and the player is less than the range
        if (distance < range)
        {
            // move the enemy towards the player at the specified speed
            /*Vector2 direction = (player.position - transform.position).normalized;
            Debug.Log(direction);
            rb.velocity = direction * speed;*/
            transform.position += Vector3.left * speed * Time.deltaTime;
            distance = Vector2.Distance(transform.position, player.position);
            if (!StopAttack)
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
                DestroyEnemy();
            }
        }
    }

    IEnumerator Attack()
    {
        StopAttack = true;
        yield return new WaitForSeconds(2.0f);
        speed = SlowSpeed;
        animator.SetBool("Attack", true);
        Instantiate(EnemyWeapon, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(2.0f);
        speed = NormalSpeed;
        animator.SetBool("Attack", false);
        
        yield return new WaitForSeconds(2.0f);
        StopAttack = false;
    }

    IEnumerator DamageAnimation()
    {
        yield return new WaitForSeconds(2.0f);
        animator.SetBool("Damage", false);
    }

    IEnumerator BulletAttacked()
    {
        animator.SetBool("Damage", true);
        StopHurting = true;
        yield return new WaitForSeconds(0.5f);
        StopHurting = false;
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
