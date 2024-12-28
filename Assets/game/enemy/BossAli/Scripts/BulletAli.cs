using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAli : MonoBehaviour
{
    public float speed = 20f;
    public GameObject impactEffect;
    private Rigidbody2D _rb;

    private void Start()
    {
        GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        Vector3 newScale = transform.localScale;

        newScale.x *= Ali.BulletAliDirection;
        _rb.linearVelocity = transform.right * Ali.BulletAliDirection * speed;
        transform.localScale = newScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Enemy.TookDamage = true;
        }

        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
