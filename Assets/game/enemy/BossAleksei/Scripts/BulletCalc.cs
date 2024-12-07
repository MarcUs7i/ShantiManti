using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCalc : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public GameObject impactEffect;
    public Vector3 desiredRotation = new Vector3(0f, 0f, 24f);
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        Vector3 rotation = desiredRotation;
        rotation.z = rotation.z * -Aleksei.BulletCalcDirection;

        rb.linearVelocity = transform.right * Aleksei.BulletCalcDirection * speed;
        transform.rotation = Quaternion.Euler(rotation);

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
