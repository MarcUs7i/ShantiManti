using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliAutomaticDoor : MonoBehaviour
{
    private Transform _door;
    private Ali _ali;
    public float speed = 5f;
    private bool _isAllowedToOpen = true;

    void Start()
    {
        _ali = FindFirstObjectByType<Ali>().GetComponent<Ali>();
        _door = GetComponent<Transform>();
    }

    void Update()
    {
        if (_ali.health <= 0 && _isAllowedToOpen)
        {
            transform.position += Vector3.up * (speed * Time.deltaTime);
            StartCoroutine(StopDoor());
        }
    }

    IEnumerator StopDoor()
    {
        yield return new WaitForSeconds(2.0f);
        _isAllowedToOpen = false;
    }
}
