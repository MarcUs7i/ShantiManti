using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBossAutomaticDoor : MonoBehaviour
{
    Transform door;
    BirdBoss birdBoss;
    public float speed = 5f;
    bool isAllowedToOpen = true;

    void Start()
    {
        door = GetComponent<Transform>();
        birdBoss = FindObjectOfType<BirdBoss>().GetComponent<BirdBoss>();
    }

    void Update()
    {
        if (birdBoss.health <= 0 && isAllowedToOpen)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            StartCoroutine(StopDoor());
        }
    }

    IEnumerator StopDoor()
    {
        yield return new WaitForSeconds(2.0f);
        isAllowedToOpen = false;
    }
}
