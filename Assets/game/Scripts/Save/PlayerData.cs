using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int savedLevels = 0;
    public int savedCoins = 0;
    public bool tutorial = false;
    public bool cloudsMove = true;

    public PlayerData ()
    {
        savedLevels = PlayerSaving.level;
        savedCoins = PlayerSaving.coins;
        tutorial = PlayerSaving.hasCompletedTutorial;
        cloudsMove = PlayerSaving.movingClouds;
    }
}
