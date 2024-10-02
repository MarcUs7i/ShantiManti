using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static bool TookDamage = false;

    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.CompareTag("Player"))
        {
            TookDamage = true;
        }
    }

}