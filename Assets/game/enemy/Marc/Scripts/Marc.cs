using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Marc : MonoBehaviour
{

    public Transform target;
    public Transform firePoint;
    public bool WalkStop = false;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    bool DestroyYourSelf = false;

    public float range = 10f; // the range at which the enemy will start moving towards the player
    public float attackRange = 8f; // the range at which the enemy will attack the player
    float distance;
    public Transform enemyGFX;
    private Transform player; // reference to the player's transform
    public GameObject EnemyWeapon;

    Path path;
    int currentWaypoint = 0;
    //bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    public Animator animator;
    bool Stop = false;
    public static float BulletConDirection;

    // Health
    public int health = 100;
    public GameObject deathEffect;

    bool TimeStopHearting = false;

    public Collider2D groundCheckCollider;

    // Start is called before the first frame update
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
            seeker.StartPath(rb.position, target.position, OnPathComplete);
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

    // Update is called once per frame
    void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, player.position);
        if (Pause.IsPause == false && distance < range && WalkStop == false)
        {
            if (path == null)
            {
                return;
            }
            if (currentWaypoint >= path.vectorPath.Count)
            {
                //reachedEndOfPath = true;
                return;
            }
            else
            {
                //reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] -rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
            //You can make look it differently, if you delete 'rb.velocity' and add 'force' instead.
            if (rb.velocity.x >= 0.01f)
            {
                enemyGFX.transform.localScale = new Vector3(-1f, 1f, 1f);
                BulletConDirection = 1f;
            }
            else if (rb.velocity.x <= -0.01f)
            {
                enemyGFX.transform.localScale = new Vector3(1f, 1f, 1f);
                BulletConDirection = 0f;
            }

            if (distance < attackRange)
            {
                if (Stop == false)
                {
                    StartCoroutine(Attack());
                }
            }

            DestroyYourSelf = true;
        }

        distance = Vector2.Distance(transform.position, player.position);
        if (distance > range && DestroyYourSelf == true)
        {
            DestroyYourSelf = false;
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
        if (collider.gameObject.tag == "Bullet" && distance < range && TimeStopHearting == false)
        {
            StartCoroutine(BulletAttacked());
            TakeDamage(40);
        }
    }

    void Update()
    {
        /*if (animator.GetBool("Damage"))
        {
            StartCoroutine(DamageAnimation());
        }*/
        /*if (Input.GetKeyDown(KeyCode.L))
		{
			StartCoroutine(Attack());
		}*/
        if (Pause.IsPause == false)
        {
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
        //Debug.Log("Attacked");
        Stop = true;
        yield return new WaitForSeconds(2.0f);
        WalkStop = true;
        animator.SetBool("Attack", true);
        Instantiate(EnemyWeapon, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(2.0f);
        WalkStop = false;
        animator.SetBool("Attack", false);
        StartCoroutine(Wait());
    }

    /*IEnumerator DamageAnimation()
    {
        yield return new WaitForSeconds(2.0f);
        animator.SetBool("Damage", false);
    }*/

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.0f);
        Stop = false;
    }

    IEnumerator BulletAttacked()
    {
        WalkStop = true;
        animator.SetBool("Damage", true);
        TimeStopHearting = true;
        yield return new WaitForSeconds(2.0f);
        TimeStopHearting = false;
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
