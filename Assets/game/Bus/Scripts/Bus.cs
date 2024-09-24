using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : MonoBehaviour
{
    public Animator animator;
    public float speed = 20f;
    bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (start == true)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            StartCoroutine(Run());
            Destroy(collider.gameObject);
        }
    }

    IEnumerator Run()
    {
        animator.SetBool("Run", true);
        start= true;
        yield return new WaitForSeconds(5.0f);
        Endlevel.nextLevel = true;
    }
}
