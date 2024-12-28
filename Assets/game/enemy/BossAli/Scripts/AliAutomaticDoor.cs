using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliAutomaticDoor : MonoBehaviour
{
    public float speed = 5f;
    private Ali _ali;
    private bool _isAllowedToOpen = true;

    private void Start()
    {
        _ali = FindFirstObjectByType<Ali>().GetComponent<Ali>();
    }

    private void FixedUpdate()
    {
        if (_ali.health > 0 || !_isAllowedToOpen)
        {
            return;
        }
        
        transform.position += Vector3.up * (speed * Time.deltaTime);
        StartCoroutine(StopDoor());
    }

    private IEnumerator StopDoor()
    {
        yield return new WaitForSeconds(2.0f);
        _isAllowedToOpen = false;
    }
}
