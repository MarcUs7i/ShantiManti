using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redCoin : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.CompareTag("Player"))
        {
            var player = c2d.GetComponent<PlayerHealth>();
            player.Die();
        }

        Destroy(gameObject);
    }
}
