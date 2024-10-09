using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAli : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public GameObject impactEffect;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        Vector3 newScale = transform.localScale;

        newScale.x *= Ali.BulletAliDirection;
        rb.velocity = transform.right * Ali.BulletAliDirection * speed;
        transform.localScale = newScale;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Enemy.TookDamage = true;
        }

        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
