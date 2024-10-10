using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCon : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public GameObject impactEffect;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * Marc.BulletConDirection * speed;
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
