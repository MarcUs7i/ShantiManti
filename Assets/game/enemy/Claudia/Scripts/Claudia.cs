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

    Path path;
    Seeker seeker;
    Transform player;
    Rigidbody2D rb;
    Transform enemyGFX;
    Animator animator;

    [Header("Health")]
    public int health = 100;
    public GameObject deathEffect;
    
    [Header("Ground Check")]
    public Collider2D groundCheckCollider;

    bool StopHurting = false;
    bool StopAttack = false;
    bool Stop = false;
    bool InNear = false;
    int currentWaypoint = 0;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        enemyGFX = GetComponentInChildren<SpriteRenderer>().transform;


        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, player.position, OnPathComplete);
        }
    }

    void OnPathComplete (Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (Pause.IsPause)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);
        if (!Stop && InNear && !StopAttack)
        {
            if (path == null)
            {
                return;
            }
            if (currentWaypoint >= path.vectorPath.Count)
            {
                return;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] -rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            rb.AddForce(force);

            distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
            
            //You can make look it differently, if you delete 'rb.velocity' and add 'force' instead.
            if (rb.linearVelocity.x >= 0.01f)
            {
                enemyGFX.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (rb.linearVelocity.x <= -0.01f)
            {
                enemyGFX.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        distance = Vector2.Distance(transform.position, player.position);
        if (distance < range)
        {
            InNear = true;
            distance = Vector2.Distance(transform.position, player.position);
            if (distance > range)
            {
                Destroy(gameObject);
            }
        }

        // check if the enemy is not on the ground
        if (!IsOnGround())
        {
            DestroyEnemy();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !StopAttack)
        {
            StartCoroutine(Attack());
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

    IEnumerator Attack()
    {
        StopAttack = true;

        animator.SetBool("Attack", true);
        Enemy.TookDamage = true;
        yield return new WaitForSeconds(2.0f);
        animator.SetBool("Attack", false);

        StopAttack = false;
    }

    IEnumerator BulletAttacked()
    {
        Stop = true;
        animator.SetBool("Damage", true);
        StopHurting = true;
        yield return new WaitForSeconds(2.0f);
        StopHurting = false;
        animator.SetBool("Damage", false);
        Stop = false;
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
