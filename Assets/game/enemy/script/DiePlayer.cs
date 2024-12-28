using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DiePlayer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.CompareTag("Player"))
        {
            c2d.GetComponent<PlayerHealth>().Die();
        }
    }
}