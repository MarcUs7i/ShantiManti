using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAng : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    public Vector3 desiredRotation0 = new Vector3(0f, 0f, 24f); // Change this to your desired rotation
    public Vector3 desiredRotation1 = new Vector3(0f, 0f, -24f); // Change this to your desired rotation

    // Start is called before the first frame update
    void Start()
    {
        if (Aleksei.BulletAngDirection == 0f)
        {
            rb.velocity = -transform.right * speed;
            transform.rotation = Quaternion.Euler(desiredRotation0);
        }
        if (Aleksei.BulletAngDirection == 1f)
        {
            rb.velocity = transform.right * speed;
            transform.rotation = Quaternion.Euler(desiredRotation1);
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
