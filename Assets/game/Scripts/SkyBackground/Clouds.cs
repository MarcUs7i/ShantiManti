using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clouds : MonoBehaviour
{
    private Rigidbody2D _rb;
    public Transform clonePoint;
    public Transform destroyPoint;
    public new Transform camera;
    public GameObject cloudPrefab;
    public float speed = 5;
    public float height = 2.064558f;
    public float spaceBetween = 15.841f;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var distanceX = destroyPoint.position.x - transform.position.x;

        if (!Pause.IsPause && CloudsToggle.CloudsCanMove)
        {
            transform.position += Vector3.right * (Time.deltaTime * speed);
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

    private void UpdateYPosition(Transform targetTransform)
    {
        var newPosition = targetTransform.position;
        newPosition.y = camera.position.y + height;
        targetTransform.position = newPosition;
    }
}
