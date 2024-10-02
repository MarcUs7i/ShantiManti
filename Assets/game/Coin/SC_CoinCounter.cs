using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_CoinCounter : MonoBehaviour
{
    Text counterText;

    void Start()
    {
        counterText = GetComponent<Text>();
    }

    void Update()
    {
        //Set the current number of coins to display
        if (counterText.text != SC_2DCoin.totalCoins.ToString())
        {
            counterText.text = SC_2DCoin.totalCoins.ToString();
        }
    }
}