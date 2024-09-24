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
        float distance = Vector2.Distance(transform.position, destroyPoint.position);
        int distanceOfDestroyPoint = (int)Math.Round(distance);
        if (!Pause.IsPause && CloudsToggle.cloudsCanMove)
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
            //rb.velocity = transform.right * speed;
            if (distanceOfDestroyPoint <= 0)
            {
                Instantiate(cloudPrefab, clonePoint.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        UpdateYPosition(transform);
        UpdateYPosition(destroyPoint);
        UpdateYPosition(clonePoint);
    }

    void UpdateYPosition(Transform targetTransform)
    {
        Vector3 newPosition = targetTransform.position;
        newPosition.y = camera.position.y + height;
        targetTransform.position = newPosition;
    }
}