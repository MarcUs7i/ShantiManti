using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKim : MonoBehaviour
{
    public float speed = 20f;
    public GameObject impactEffect;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocity = transform.right * Kimberley.BulletKimDirection * speed;
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
