using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int savedLevels;
    public int savedCoins;
    public bool tutorial;
    public bool cloudsMove;

    public PlayerData()
    {
        savedLevels = PlayerSaving.Level;
        savedCoins = PlayerSaving.Coins;
        tutorial = PlayerSaving.HasCompletedTutorial;
        cloudsMove = PlayerSaving.MovingClouds;
    }
}
