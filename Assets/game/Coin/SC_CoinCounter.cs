using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_CoinCounter : MonoBehaviour
{
    private Text _counterText;

    private void Start()
    {
        _counterText = GetComponent<Text>();
    }

    private void Update()
    {
        //Set the current number of coins to display
        if (_counterText.text != SC_2DCoin.TotalCoins.ToString())
        {
            _counterText.text = SC_2DCoin.TotalCoins.ToString();
        }
    }
}