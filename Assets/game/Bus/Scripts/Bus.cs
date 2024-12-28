using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : MonoBehaviour
{
    private static readonly int RunAnimationID = Animator.StringToHash("Run");
    private Animator _animator;
    public float speed = 20f;
    private bool _moveBus;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (_moveBus)
        {
            transform.position += Vector3.right * (speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Run());
            Destroy(collider.gameObject);
        }
    }

    private IEnumerator Run()
    {
        _animator.SetBool(RunAnimationID, true);
        _moveBus = true;
        yield return new WaitForSeconds(5.0f);
        
        Endlevel endLevelInstance = FindFirstObjectByType<Endlevel>().GetComponent<Endlevel>();
        endLevelInstance.GoToNextLevel();
    }
}
