using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBossAutomaticDoor : MonoBehaviour
{
    public float speed = 5f;
    private BirdBoss _birdBoss;
    private bool _isAllowedToOpen = true;

    private void Start()
    {
        _birdBoss = FindFirstObjectByType<BirdBoss>().GetComponent<BirdBoss>();
    }

    private void FixedUpdate()
    {
        if (_birdBoss.health > 0 || !_isAllowedToOpen)
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
