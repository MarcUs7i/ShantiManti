using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNugget : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        if (Firnschliff.BulletNuggetDirection == 0f)
        {
            rb.velocity = -transform.right * speed;
        }
        if (Firnschliff.BulletNuggetDirection == 1f)
        {
            rb.velocity = transform.right * speed;
        }
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
