using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCounter : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Disabeling());
    }

    IEnumerator Disabeling()
    {
        yield return new WaitForSeconds(2.0f);
        animator.SetBool("Fade", true);

        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Fade", false);

        yield return new WaitForSeconds(1.8f);
        Destroy(gameObject);
    }
}
