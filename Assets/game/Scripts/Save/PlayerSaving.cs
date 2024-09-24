using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaving : MonoBehaviour
{
    public static int levels = 0;
    public static int savedCoins = 0;
    public static int tutorial = 0;
    public static int cloudMove = 0;
    public static bool SavingPlayer = false;
    public static bool LoadingPlayer = false;
    public static bool DeleteingPlayer = false;
    public static bool Deleteing = false;

    void Update()
    {
        /*if (levels != Endlevel.level)
        {
            Endlevel.level = levels;
        }
        if (savedCoins != SC_2DCoin.totalCoins && Loading == false)
        {
            SC_2DCoin.totalCoins = savedCoins;
        }*/
        if(SavingPlayer == true)
        {
            SavingPlayer = false;
            SavePlayer();
        }
        if (LoadingPlayer == true)
        {
            LoadingPlayer = false;
            LoadPlayer();
        }
        if (DeleteingPlayer == true)
        {
            DeleteingPlayer = false;
            DeletePlayer();
        }
        /*if (Input.GetKeyDown(KeyCode.L))
        {
            levels = 1;
            SavingPlayer = true;
        }*/
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
        Debug.Log("Saved");
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        levels = data.level;
        savedCoins = data.coins;
        tutorial = data.tutorial;
        cloudMove = data.cloudsMoveData;

        Debug.Log("Loaded");
        SC_2DCoin.totalCoins = savedCoins;
        Endlevel.level = levels;
    }

    public void DeletePlayer()
    {
        Deleteing = true;
        savedCoins = 0;
        levels = 0;
        tutorial = 0;
        cloudMove = 0;

        // For MusicToggle.cs
        PlayerPrefs.SetInt("MusicToggled", 1);
        PlayerPrefs.Save();

        // For SoundBar.cs
        PlayerPrefs.SetFloat("SoundVolume", 0.75f);
        PlayerPrefs.Save();

        SaveSystem.SavePlayer(this);
        Debug.Log("Deleted");
    }
}
