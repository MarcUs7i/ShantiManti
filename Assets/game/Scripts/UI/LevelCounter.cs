using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCounter : MonoBehaviour
{
    private static readonly int FadeAnimationID = Animator.StringToHash("Fade");
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(Disabling());
    }

    private IEnumerator Disabling()
    {
        yield return new WaitForSeconds(2.0f);
        _animator.SetBool(FadeAnimationID, true);

        yield return new WaitForSeconds(0.1f);
        _animator.SetBool(FadeAnimationID, false);

        yield return new WaitForSeconds(1.8f);
        Destroy(gameObject);
    }
}
