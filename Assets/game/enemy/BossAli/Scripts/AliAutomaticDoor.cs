using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliAutomaticDoor : MonoBehaviour
{
    Transform door;
    Ali ali;
    public float speed = 5f;
    bool isAllowedToOpen = true;

    void Start()
    {
        ali = FindObjectOfType<Ali>().GetComponent<Ali>();
        door = GetComponent<Transform>();
    }

    void Update()
    {
        if (ali.health <= 0 && isAllowedToOpen)
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
