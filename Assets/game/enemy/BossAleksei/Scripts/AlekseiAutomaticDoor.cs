using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlekseiAutomaticDoor : MonoBehaviour
{
    public float speed = 5f;
    private Aleksei _aleksei;
    private bool _isAllowedToOpen = true;

    private void Start()
    {
        _aleksei = FindFirstObjectByType<Aleksei>().GetComponent<Aleksei>();
    }

    private void FixedUpdate()
    {
        if (_aleksei.health > 0 || !_isAllowedToOpen)
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
