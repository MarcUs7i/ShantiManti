using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_2DCoin : MonoBehaviour
{
    //Keep track of total picked coins (Since the value is static, it can be accessed at "SC_2DCoin.totalCoins" from any script)
    public static int TotalCoins;

    //Coin Cooldown
    private bool _canPickUp = true;
    public float pickUpCooldown = 0.1f;

    private void Awake()
    {
        //Make Collider2D as trigger 
        GetComponent<Collider2D>().isTrigger = true;
        TotalCoins = PlayerSaving.coins;
    }

    private void OnTriggerEnter2D(Collider2D c2d)
    {
        if (_canPickUp && (c2d.CompareTag("Player") || c2d.CompareTag("Bullet")))
        {
            _canPickUp = false; // Disable picking up coins temporarily
            
            TotalCoins++;

            c2d.GetComponent<PlayerMovement>().PlayCoinSound();
            Destroy(gameObject);
    
            // Start the cooldown coroutine
            StartCoroutine(CoinPickUpCooldown());
        }
    }

    private IEnumerator CoinPickUpCooldown()
    {
        yield return new WaitForSeconds(pickUpCooldown);
        _canPickUp = true; //Enable coin pickup again after the cooldown
    }

    private void Update()
    {
        if (TotalCoins != PlayerSaving.coins)
        {
            PlayerSaving.coins = TotalCoins;
            PlayerSaving.SavePlayer();
        }
    }
}