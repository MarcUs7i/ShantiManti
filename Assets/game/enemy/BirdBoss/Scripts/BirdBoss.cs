using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BirdBoss : MonoBehaviour
{
    public Transform target;

    [Header("Speed & Range")]
    public float defaultSpeed = 300f;
    public float StartRange = 20f;
    public float attackableRange = 10f;

    [Header("stage2")]
    public float stage2Speed = 700f;
    public float stage2AnimSec = 2.0f;

    [Header("Pathfinding")]
    public float nextWaypointDistance = 3f;
    int currentWaypoint = 0;

    [Header("Audio")]
    public AudioSource BackgroundMusic;
    AudioSource audioSFX;

    [Header("Health")]
    public int health = 100;
    public GameObject deathEffect;

    Path path;
    Animator animator;
    Transform enemyGFX;
    Seeker seeker;
    Rigidbody2D rb;

    bool StopHurting = false;
    bool isInStage2 = false;
    bool transitioningToStage2 = false;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        enemyGFX = GetComponentInChildren<SpriteRenderer>().transform;

        audioSFX = GetComponent<AudioSource>();

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

    void FixedUpdate()
    {
        if(Pause.IsPause)
        {
            return;
        }
        float distance = Vector2.Distance(transform.position, target.position);

        if (health <= 25 && !isInStage2)
        {
            StartCoroutine(GetToStage2());
        }  

        if (!transitioningToStage2 && distance < StartRange)
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
            Vector2 force = direction * defaultSpeed * Time.deltaTime;

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
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        float distance = Vector2.Distance(transform.position, target.position);
        if (collider.gameObject.tag == "Bullet" && distance < attackableRange && !StopHurting)
        {
            TakeDamage(10);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Attack());
        }
    }

    // Health
    void TakeDamage(int damage)
	{
		if (!MainMenu.ExitLevel)
		{
			health -= damage;

			StartCoroutine(DamageAnimation());
            StartCoroutine(BulletAttacked());

			if (health <= 0)
			{
                // Die
				Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
			}
		}
	}

    IEnumerator Attack()
    {
        animator.SetBool("Attack", true);
        Enemy.TookDamage = true;
        yield return new WaitForSeconds(2.0f);
        animator.SetBool("Attack", false);
    }

    IEnumerator DamageAnimation()
    {
        animator.SetBool("Damage", true);
        yield return new WaitForSeconds(2.0f);
        animator.SetBool("Damage", false);
    }

    IEnumerator GetToStage2()
    {
        StopHurting = true;
        isInStage2 = true;

        animator.SetBool("EnterStage2", true);
        transitioningToStage2 = true;
        StartCoroutine(Music());

        defaultSpeed = stage2Speed;
        yield return new WaitForSeconds(stage2AnimSec);

        transitioningToStage2 = false;
        StopHurting = false;
    }

    IEnumerator BulletAttacked()
    {
        StopHurting = true;
        yield return new WaitForSeconds(2.0f);
        StopHurting = false;
    }

    IEnumerator Music()
    {
        float oldVolume = BackgroundMusic.volume;

        BackgroundMusic.volume = 0.25f;
        audioSFX.Play();

        yield return new WaitForSeconds(3.0f);
        BackgroundMusic.volume = oldVolume;
    }
}
