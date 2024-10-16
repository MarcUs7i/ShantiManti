using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_2DCoin : MonoBehaviour
{
    //Keep track of total picked coins (Since the value is static, it can be accessed at "SC_2DCoin.totalCoins" from any script)
    public static int totalCoins = 0;
    public static bool playCoinSound = false; 

    //Coin Cooldown
    private bool canPickUp = true;
    public float pickUpCooldown = 0.1f;

    void Awake()
    {
        //Make Collider2D as trigger 
        GetComponent<Collider2D>().isTrigger = true;
        totalCoins = PlayerSaving.coins;
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (canPickUp && (c2d.CompareTag("Player") || c2d.CompareTag("Bullet")))
        {
            canPickUp = false; // Disable picking up coins temporarily
            
            totalCoins++;

            playCoinSound = true;
            Destroy(gameObject);
    
            // Start the cooldown coroutine
            StartCoroutine(CoinPickUpCooldown());
        }
    }

    IEnumerator CoinPickUpCooldown()
    {
        yield return new WaitForSeconds(pickUpCooldown);
        canPickUp = true; //Enable coin pickup again after the cooldown
    }

    void Update()
    {
        if (totalCoins != PlayerSaving.coins)
        {
            PlayerSaving.coins = totalCoins;
            PlayerSaving.SavePlayer();
        }
    }
}