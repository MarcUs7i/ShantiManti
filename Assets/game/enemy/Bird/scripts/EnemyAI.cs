using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    private Transform _enemyGfx;
    private Path _path;
    private int _currentWaypoint;
    private Seeker _seeker;
    private Rigidbody2D _rb;
    
    // ↓ uncomment every comment with reachedEndOfPath if you need it. In this case it isn't useful
    //bool reachedEndOfPath = false;

    private void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();
        _enemyGfx = GetComponentInChildren<SpriteRenderer>().transform;

        InvokeRepeating(nameof(UpdatePath), 0f, .5f);
    }

    private void UpdatePath()
    {
        if (_seeker.IsDone())
        {
            _seeker.StartPath(_rb.position, target.position, OnPathComplete);
        }
    }

    private void OnPathComplete (Path p)
    {
        if (p.error)
        {
            return;
        }
        _path = p;
        _currentWaypoint = 0;
    }

    private void FixedUpdate()
    {
        if (Pause.IsPause)
        {
            return;
        }
        
        if (_path == null)
        {
            return;
        }
        if (_currentWaypoint >= _path.vectorPath.Count)
        {
            //reachedEndOfPath = true;
            return;
        }
        //reachedEndOfPath = false;
        
        Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] -_rb.position).normalized;
        Vector2 force = direction * (speed * Time.deltaTime);

        _rb.AddForce(force);

        var distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            _currentWaypoint++;
        }
        //You can make look it differently, if you delete 'rb.velocity' and add 'force' instead.
        if (_rb.linearVelocity.x >= 0.01f)
        {
            _enemyGfx.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (_rb.linearVelocity.x <= -0.01f)
        {
            _enemyGfx.transform.localScale = new Vector3(1f, 1f, 1f);
        }

    }
}
