using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlekseiAutomaticDoor : MonoBehaviour
{
    public Transform door;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Aleksei.AlekseiDead == true)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            StartCoroutine(StopDoor());
        }
    }

    IEnumerator StopDoor()
    {
        yield return new WaitForSeconds(2.0f);
        Aleksei.AlekseiDead = false;
    }
}
