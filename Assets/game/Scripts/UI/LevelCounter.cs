using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCounter : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Disabeling());
    }

    IEnumerator Disabeling()
    {
        yield return new WaitForSeconds(2.0f);
        animator.SetBool("Disable", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Disable", false);
        yield return new WaitForSeconds(1.8f);
        Destroy(gameObject);
    }
}
