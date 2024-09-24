using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rettung : MonoBehaviour
{
    public Animator animator;
    public Animator GFX;
    public static bool runScript = false;
    public static bool finished = false;
    public float wait1 = 2.0f;
    public float wait2 = 2.0f;
    public float wait3 = 2.0f;
    public float waitBefore = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        runScript = false;
        finished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (runScript)
        {
            runScript = false;
            StartCoroutine(Script());
        }
    }

    IEnumerator Script()
    {
        yield return new WaitForSeconds(waitBefore);
        animator.SetBool("Run", true);
        GFX.SetBool("Stop", false);
        yield return new WaitForSeconds(wait1);
        animator.SetBool("Run", false);
        GFX.SetBool("Stop", true);
        animator.SetBool("IdleAfter", true);
        finished = true;
        yield return new WaitForSeconds(wait2);
        animator.SetBool("IdleAfter", false);
        GFX.SetBool("Stop", false);
        animator.SetBool("RunAfter", true);
        yield return new WaitForSeconds(wait3);
        animator.SetBool("RunAfter", false);
        GFX.SetBool("Stop", true);
        animator.SetBool("IdleAfterAfter", true);
        
    }
}
