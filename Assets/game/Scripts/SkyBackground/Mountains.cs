using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Mountains : MonoBehaviour
{
    private float _horizontalMountainMove;
    private Rigidbody2D _rb;
    public Transform clonePoint;
    public Transform destroyPoint;
    //public Transform camera;
    public GameObject mountain;
    public float speed = 5;
    //public float height = 0.96f;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!Mathf.Approximately(_horizontalMountainMove / 2, PlayerMovement.HorizontalMove))
        {
            _horizontalMountainMove = PlayerMovement.HorizontalMove / 2;
        }

        var distance = Vector2.Distance(transform.position, destroyPoint.position);
        var distanceOfDestroyPoint = (int)Math.Round(distance);
        if (!Pause.IsPause)
        {
            transform.position += Vector3.left * (_horizontalMountainMove * Time.deltaTime * speed);
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
