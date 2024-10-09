using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlekseiAutomaticDoor : MonoBehaviour
{
    Transform door;
    Aleksei aleksei;
    public float speed = 5f;
    bool isAllowedToOpen = true;

    void Start()
    {
        aleksei = FindObjectOfType<Aleksei>().GetComponent<Aleksei>();
        door = GetComponent<Transform>();
    }

    void Update()
    {
        if (aleksei.health <= 0 && isAllowedToOpen)
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
