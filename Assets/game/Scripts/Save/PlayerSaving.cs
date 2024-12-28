using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaving : MonoBehaviour
{
    public static int Level;
    public static int Coins;
    public static bool HasCompletedTutorial;
    public static bool MovingClouds = true;

    private void Awake()
    {
        LoadPlayer();
    }

    public static void SavePlayer()
    {
        SaveSystem.SavePlayer();
        Debug.Log($"Saved level: {Level}, coins: {Coins}, tutorial: {HasCompletedTutorial}, cloudsMove: {MovingClouds}");
    }

    public static void LoadPlayer()
    {
        var data = SaveSystem.LoadPlayer();

        Level = data.savedLevels;
        Coins = data.savedCoins;
        HasCompletedTutorial = data.tutorial;
        MovingClouds = data.cloudsMove;

        Debug.Log($"Loaded level: {Level}, coins: {Coins}, tutorial: {HasCompletedTutorial}, cloudsMove: {MovingClouds}");
        SC_2DCoin.TotalCoins = Coins;
        Endlevel.level = Level;
    }

    public static void DeletePlayer()
    {
        Level = 0;
        Coins = 0;
        HasCompletedTutorial = false;
        MovingClouds = true;

        // For MusicToggle.cs
        PlayerPrefs.SetInt("MusicToggled", 1);
        PlayerPrefs.Save();

        // For SoundBar.cs
        PlayerPrefs.SetFloat("SoundVolume", 0.75f);
        PlayerPrefs.Save();

        SaveSystem.SavePlayer();
        Debug.Log("Deleted");
    }
}
