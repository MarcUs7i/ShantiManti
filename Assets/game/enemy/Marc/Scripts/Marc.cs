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
    int currentWaypoint = 0;

    [Header("Health")]
    public int health = 100;
    public GameObject deathEffect;

    [Header("Weapon")]
    public Transform firePoint;
    public GameObject EnemyWeapon;

    [Header("")]
    public Collider2D groundCheckCollider;
    public Transform enemyGFX;

    Path path;
    Seeker seeker;
    Rigidbody2D rb;
    Transform player;
    Animator animator;

    public static float BulletConDirection;
    bool StopAttack = false;
    bool StopHurting = false;
    bool DestroyYourSelf = false;
    bool WalkStop = false;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

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

    void FixedUpdate()
    {
        if (Pause.IsPause)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < range && !WalkStop)
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
                BulletConDirection = 1f;
            }
            else if (rb.linearVelocity.x <= -0.01f)
            {
                enemyGFX.transform.localScale = new Vector3(1f, 1f, 1f);
                BulletConDirection = -1f;
            }

            if (distance < attackRange)
            {
                if (!StopAttack)
                {
                    StartCoroutine(Attack());
                }
            }

            DestroyYourSelf = true;
        }

        if (!IsOnGround())
        {
            // destroy the enemy game object
            DestroyEnemy();
        }

        // destroy the enemy game object if it is out of range
        distance = Vector2.Distance(transform.position, player.position);
        if (distance > range && DestroyYourSelf)
        {
            Destroy(gameObject);
        }
        
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
            TakeDamage(40);
        }
    }

    IEnumerator Attack()
    {
        //Debug.Log("Attacked");
        StopAttack = true;
        yield return new WaitForSeconds(2.0f);
        WalkStop = true;
        animator.SetBool("Attack", true);
        Instantiate(EnemyWeapon, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(2.0f);
        WalkStop = false;
        animator.SetBool("Attack", false);
        
        yield return new WaitForSeconds(2.0f);
        StopAttack = false;
    }

    IEnumerator BulletAttacked()
    {
        WalkStop = true;
        animator.SetBool("Damage", true);
        StopHurting = true;
        yield return new WaitForSeconds(2.0f);
        StopHurting = false;
        WalkStop = false;
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
