using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Aleksei : MonoBehaviour
{

    public Transform target;
    public Transform firePoint;
    public float SlowSpeed = 300f;
    public float NormalSpeed = 500f;
    public float FastSpeed = 700f;
    public float speed = 500f;
    public float encourageSpeed = 900f;
    public float nextWaypointDistance = 3f;
    public AudioSource BackgroundMusic;
    public AudioSource audioSource;
    bool IsPlay = false;


    // Health
    public int health = 200;
    bool StopHearting = false;
    bool TimeStopHearting = false;
    public static bool AlekseiDead = false;

    public GameObject deathEffect;

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
    bool StopAttack = false;
    bool NormalSpeeding = false;
    public static float BulletAngDirection;
    bool FirstStop = true;
    bool Attacking = false;
    public float encourageSec = 2.0f;
    float encourageStart = 1f;
    bool Stop = false;

    bool DamageAnim = false;

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

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Pause.IsPause == false && FirstStop == false && Attacking == false && Stop == false)
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
                BulletAngDirection = 1f;
            }
            else if (rb.velocity.x <= -0.01f)
            {
                enemyGFX.transform.localScale = new Vector3(1f, 1f, 1f);
                BulletAngDirection = 0f;
            }

            if (distance < attackRange)
            {
                if (StopAttack == false)
                {
                    StartCoroutine(Attack());
                }
            }

        }

        distance = Vector2.Distance(transform.position, player.position);
        if (distance > range)
        {
            speed = FastSpeed;
        }
        if (distance < range)
        {
            FirstStop = false;
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(ColAttack());
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bullet" && distance < range && StopHearting == false && TimeStopHearting == false)
        {
            TakeDamage(10);
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
        AlekseiDead = true;
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    // Health End
    void Update()
    {
        if (Pause.IsPause == false)
        {
            /*if (animator.GetBool("Damage"))
            {
                StartCoroutine(DamageAnimation());
            }*/
            if (distance < range && NormalSpeeding == true)
            {
                NormalSpeeding = false;
                speed = NormalSpeed;
            }
            /*if (Input.GetKeyDown(KeyCode.Q))
            {
                TakeDamage(20);
            }*/
            if (health <= 60 && encourageStart == 1f)
            {
                StartCoroutine(Encourage());
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
        //Debug.Log("Attacked");
        StopAttack = true;
        yield return new WaitForSeconds(2.0f);
        Attacking = true;
        animator.SetBool("Attack", true);
        Instantiate(EnemyWeapon, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(2.0f);
        animator.SetBool("Attack", false);
        Attacking = false;
        StartCoroutine(Wait());
    }

    IEnumerator ColAttack()
    {
        animator.SetBool("SwordAttack", true);
        Enemy.TakedDamage = true;
        yield return new WaitForSeconds(2.0f);
        animator.SetBool("SwordAttack", false);
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

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.0f);
        StopAttack = false;
    }

    IEnumerator SlowDown()
    {
        speed = SlowSpeed;
        yield return new WaitForSeconds(2.0f);
        NormalSpeeding = true;
    }

    IEnumerator Encourage()
    {
        Debug.Log("Enc");
        StopHearting = true;
        encourageStart++;
        animator.SetBool("Encourage", true);
        Stop = true;
        IsPlay = true;
        NormalSpeed = FastSpeed;
        FastSpeed = encourageSpeed;
        yield return new WaitForSeconds(encourageSec);
        animator.SetBool("Encourage", false);
        Stop = false;
        StopHearting = false;
        DamageAnim = false;
    }

    IEnumerator BulletAttacked()
    {
        TimeStopHearting = true;
        yield return new WaitForSeconds(1.5f);
        TimeStopHearting = false;
    }

    IEnumerator Music()
    {
        BackgroundMusic.volume = 0.25f;
        audioSource.Play();
        yield return new WaitForSeconds(3.0f);
        BackgroundMusic.volume = 1f;
    }
}
