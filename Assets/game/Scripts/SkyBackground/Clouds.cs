using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clouds : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform clonePoint;
    public Transform destroyPoint;
    new public Transform camera;
    public GameObject cloudPrefab;
    public float speed = 5;
    public float height = 2.064558f;
    public float spaceBetween = 15.841f;
    //public float maxCloudClonesAtStart = 200;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distanceX = destroyPoint.position.x - transform.position.x;

        if (!Pause.IsPause && CloudsToggle.cloudsCanMove)
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
            //rb.velocity = transform.right * speed;
            if (distanceX <= 0)
            {
                Instantiate(cloudPrefab, clonePoint.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        UpdateYPosition(transform);
        UpdateYPosition(clonePoint);
    }

    void UpdateYPosition(Transform targetTransform)
    {
        Vector3 newPosition = targetTransform.position;
        newPosition.y = camera.position.y + height;
        targetTransform.position = newPosition;
    }
}
