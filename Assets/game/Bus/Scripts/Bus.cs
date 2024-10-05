using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : MonoBehaviour
{
    private Animator animator;
    public float speed = 20f;
    bool moveBus = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (moveBus)
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
        moveBus = true;
        yield return new WaitForSeconds(5.0f);
        
        Endlevel endLevelInstance = FindObjectOfType<Endlevel>().GetComponent<Endlevel>();
        endLevelInstance.GoToNextLevel();
    }
}
