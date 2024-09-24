using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Mountains : MonoBehaviour
{
    float horizontalMountainMove = 0;
    private Rigidbody2D rb;
    public Transform clonePoint;
    public Transform destroyPoint;
    //public Transform camera;
    public GameObject mountain;
    public float speed = 5;
    //public float height = 0.96f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (horizontalMountainMove / 2 != PlayerMovement.horizontalMove)
        {
            horizontalMountainMove = PlayerMovement.horizontalMove / 2;
        }

        float distance = Vector2.Distance(transform.position, destroyPoint.position);
        int distanceOfDestroyPoint = (int)Math.Round(distance);
        if (!Pause.IsPause)
        {
            transform.position += Vector3.left * horizontalMountainMove * Time.deltaTime * speed;
            //rb.velocity = transform.right * speed * horizontalMountainMove;
            if (distanceOfDestroyPoint <= 0)
            {
                Instantiate(mountain, clonePoint.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        /*UpdateYPosition(transform);
        UpdateYPosition(destroyPoint);
        UpdateYPosition(clonePoint);*/
    }

    /*void UpdateYPosition(Transform targetTransform)
    {
        Vector3 newPosition = targetTransform.position;
        newPosition.y = camera.position.y + height;
        targetTransform.position = newPosition;
    }*/
}
