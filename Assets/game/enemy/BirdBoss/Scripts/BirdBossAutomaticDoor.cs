using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBossAutomaticDoor : MonoBehaviour
{
    public Transform door;
    public float speed = 5f;
    void Update()
    {
        if (BirdBoss.BirdDead == true)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            StartCoroutine(StopDoor());
        }
    }

    IEnumerator StopDoor()
    {
        yield return new WaitForSeconds(2.0f);
        BirdBoss.BirdDead = false;
    }
}
