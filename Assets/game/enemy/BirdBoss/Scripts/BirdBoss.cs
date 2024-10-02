using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BirdBoss : MonoBehaviour
{
    public Animator animator;

    public Transform target;
    public float SlowSpeed = 100f;
    public float NormalSpeed = 300f;
    public float FastSpeed = 500f;
    public float speed = 300f;
    public float encourageSpeed = 700f;
    public float encourageSec = 2.0f;
    public float nextWaypointDistance = 3f;
    public AudioSource BackgroundMusic;
    public AudioSource audioSource;
    bool IsPlay = false;


    public Transform enemyGFX;

    Path path;
    int currentWaypoint = 0;
    bool start = false;
    public float StartRange = 20f;

    Seeker seeker;
    Rigidbody2D rb;

    //health
    public int health = 100;
    public GameObject deathEffect;
    public static bool BirdDead = false;
    bool StopHearting = false;
    bool TimeStopHearting = false;

    float encourageStart = 1f;
    bool DamageAnim = false;
    float distance;
    public float range = 10f;

    bool Stop = false;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

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
        if (Pause.IsPause == false && Stop == false && start == true)
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

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            //You can make look it differently, if you delete 'rb.velocity' and add 'force' instead.
            if (rb.velocity.x >= 0.01f)
            {
                enemyGFX.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (rb.velocity.x <= -0.01f)
            {
                enemyGFX.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bullet" && distance < range && StopHearting == false && TimeStopHearting == false)
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
		if (MainMenu.ExitLevel == false)
		{
			health -= damage;

			StartCoroutine(DamageAnimation());
            StartCoroutine(BulletAttacked());

			if (health <= 0)
			{
				Die();
			}
		}
	}

    void Die ()
    {
        BirdDead = true;
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    // Health End

    void Update()
    {
        if (Pause.IsPause == false)
        {
            if (health <= 25 && encourageStart == 1f)
            {
                StartCoroutine(Encourage());
            }

            if (start == false)
            {
                float distancce = Vector2.Distance(transform.position, target.position);

                if (distancce < StartRange)
                {
                    //Debug.Log("started");
                    start = true;
                }
            }
            if (IsPlay == true)
		    {
			    StartCoroutine(Music());
			    IsPlay = false;
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
        if (health <= 60 && encourageStart == 1f)
        {
            DamageAnim = true;
        }
        if (DamageAnim == false)
        {
            animator.SetBool("Damage", true);
            yield return new WaitForSeconds(2.0f);
            animator.SetBool("Damage", false);
        }
    }

    IEnumerator Encourage()
    {
        StopHearting = true;
        encourageStart++;
        animator.SetBool("Encourage", true);
        Stop = true;
        IsPlay = true;
        NormalSpeed = FastSpeed;
        speed = encourageSpeed;
        yield return new WaitForSeconds(encourageSec);
        //animator.SetBool("Encourage", false);
        Stop = false;
        StopHearting = false;
        DamageAnim = false;
    }

    IEnumerator BulletAttacked()
    {
        TimeStopHearting = true;
        yield return new WaitForSeconds(2.0f);
        TimeStopHearting = false;
    }

    IEnumerator Music()
    {
        BackgroundMusic.volume = 0.25f;
        audioSource.Play();
        yield return new WaitForSeconds(3.0f);
        BackgroundMusic.volume = 0.75f;
    }
}
