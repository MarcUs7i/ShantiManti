using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level = 0;
    public int coins = 0;
    public int tutorial = 0;
    public int cloudsMoveData = 0;

    public PlayerData (PlayerSaving player)
    {
        level = PlayerSaving.levels;
        coins = PlayerSaving.savedCoins;
        tutorial = PlayerSaving.tutorial;
        cloudsMoveData = PlayerSaving.cloudMove;
    }
}
