using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCalc : MonoBehaviour
{
    public float speed = 20f;
    public GameObject impactEffect;
    public Vector3 desiredRotation = new Vector3(0f, 0f, 24f);
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        Vector3 rotation = desiredRotation;
        rotation.z *= -Aleksei.BulletCalcDirection;

        _rb.linearVelocity = transform.right * Aleksei.BulletCalcDirection * speed;
        transform.rotation = Quaternion.Euler(rotation);

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
