using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAli : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Vector3 newScale = transform.localScale;
        if (Ali.BulletAliDirection == 0f)
        {
            rb.velocity = -transform.right * speed;
            newScale.x *= -1; // Flipping along the X-axis
        }
        if (Ali.BulletAliDirection == 1f)
        {
            rb.velocity = transform.right * speed;
        }
        transform.localScale = newScale;
    }

    public void Flip()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1; // Flipping along the X-axis
        transform.localScale = newScale;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Enemy.TakedDamage = true;
        }

        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
