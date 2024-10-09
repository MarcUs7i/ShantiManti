using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Ali : MonoBehaviour
{
    public Transform firePoint;

    [Header("Speed")]
    public float NormalSpeed = 500f;
    public float FastSpeed = 700f;
    float speed = 500f;

    [Header("Range")]
    public float range = 10f;
    public float attackRange = 8f;
    public float SwordAttackRange = 2f;

    [Header("Stage2")]
    public float stage2Speed = 900f;
    public float stage2AnimSec = 2.0f;

    [Header("Pathfinding")]
    public float nextWaypointDistance = 3f;
    int currentWaypoint = 0;

    [Header("Audio")]
    public AudioSource BackgroundMusic;
    private AudioSource audioSFX;

    [Header("Health")]
    public int health = 200;
    bool StopHurting = false;

    [Header("GameObjects")]
    public GameObject deathEffect;
    public GameObject EnemyWeapon;

    Path path;
    Seeker seeker;
    Transform player;
    Rigidbody2D rb;
    Animator animator;
    Transform enemyGFX;

    public static float BulletAliDirection;
    bool StopAttack = false;
    bool Attacking = false;
    bool isInStage2 = false;
    bool transitioningToStage2 = false;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        audioSFX = GetComponent<AudioSource>();
        enemyGFX = GetComponentInChildren<SpriteRenderer>().transform;
        animator = enemyGFX.GetComponent<Animator>();

        speed = NormalSpeed;

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
        if (distance < range && !Attacking && !transitioningToStage2)
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
            if (rb.velocity.x >= 0.01f)
            {
                enemyGFX.transform.localScale = new Vector3(-1f, 1f, 1f);
                BulletAliDirection = 1f;
            }
            else if (rb.velocity.x <= -0.01f)
            {
                enemyGFX.transform.localScale = new Vector3(1f, 1f, 1f);
                BulletAliDirection = -1f;
            }

            float AttackDec = Mathf.Round(UnityEngine.Random.Range(0.0f, 1.0f));

            distance = Vector2.Distance(transform.position, player.position);
            if (distance < attackRange && AttackDec == 0f)
            {
                if (!StopAttack)
                {
                    StartCoroutine(Attack());
                }
            }
            if (distance < SwordAttackRange && AttackDec == 1f)
            {
                if (!StopAttack)
                {
                    StartCoroutine(SwordAttack());
                }
            }
        }

        distance = Vector2.Distance(transform.position, player.position);
        if (distance > range)
        {
            speed = FastSpeed;
        }

        /*if (Input.GetKeyDown(KeyCode.O))
        {
            TakeDamage(20);
        }*/
        if (health <= 60 && !isInStage2)
        {
            StartCoroutine(GetToStage2());
        }    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(CollisionAttack());
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (collider.gameObject.tag == "Bullet" && distance < range && !StopHurting)
        {
            TakeDamage(10);
        }
    }

    // Health
    void TakeDamage(int damage)
	{
		if (!MainMenu.ExitLevel)
		{
			health -= damage;

			//only animate damage if health is above 60 (not stage2)
            if (health > 60)
			{
                StartCoroutine(DamageAnimation());
            }

            StartCoroutine(BulletAttacked());

			if (health <= 0)
			{
				//Die
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
			}
		}
	}

    IEnumerator Attack()
    {
        StopAttack = true;
        yield return new WaitForSeconds(2.0f);

        Attacking = true;
        animator.SetBool("Attack", true);
        Instantiate(EnemyWeapon, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(2.0f);

        animator.SetBool("Attack", false);
        Attacking = false;
        
        yield return new WaitForSeconds(2.0f);
        StopAttack = false;
    }

    IEnumerator SwordAttack()
    {
        StopAttack = true;

        yield return new WaitForSeconds(1.9f);
        animator.SetBool("SwordAttack", true);
        yield return new WaitForSeconds(0.1f);

        Attacking = true;
        Enemy.TookDamage = true;

        yield return new WaitForSeconds(2.0f);
        animator.SetBool("SwordAttack", false);
        
        Attacking = false;
        
        yield return new WaitForSeconds(2.0f);
        StopAttack = false;
    }

    IEnumerator CollisionAttack()
    {
        animator.SetBool("SwordAttack", true);
        Enemy.TookDamage = true;
        yield return new WaitForSeconds(2.0f);
        animator.SetBool("SwordAttack", false);
    }

    IEnumerator DamageAnimation()
    {
        animator.SetBool("Damage", true);
        yield return new WaitForSeconds(2.0f);
        animator.SetBool("Damage", false);
    }

    IEnumerator GetToStage2()
    {
        isInStage2 = true;

        transitioningToStage2 = true;
        StopHurting = true;
        animator.SetBool("EnterStage2", true);

        StartCoroutine(Music());
        NormalSpeed = FastSpeed;
        FastSpeed = stage2Speed;

        yield return new WaitForSeconds(stage2AnimSec);
        animator.SetBool("EnterStage2", false);

        transitioningToStage2 = false;
        StopHurting = false;
        DamageAnim = false;
    }

    IEnumerator BulletAttacked()
    {
        StopHurting = true;
        yield return new WaitForSeconds(1.5f);
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
