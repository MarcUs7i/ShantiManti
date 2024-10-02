using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaving : MonoBehaviour
{
    public static int level = 0;
    public static int coins = 0;
    public static bool hasCompletedTutorial = false;
    public static bool movingClouds = true;
    public static bool Deleting = false;

    void Awake()
    {
        LoadPlayer();
    }

    public static void SavePlayer()
    {
        SaveSystem.SavePlayer();
        Debug.Log($"Saved level: {level}, coins: {coins}, tutorial: {hasCompletedTutorial}, cloudsMove: {movingClouds}");
    }

    public static void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        level = data.savedLevels;
        coins = data.savedCoins;
        hasCompletedTutorial = data.tutorial;
        movingClouds = data.cloudsMove;

        Debug.Log($"Loaded level: {level}, coins: {coins}, tutorial: {hasCompletedTutorial}, cloudsMove: {movingClouds}");
        SC_2DCoin.totalCoins = coins;
        Endlevel.level = level;
    }

    public static void DeletePlayer()
    {
        Deleting = true;
        level = 0;
        coins = 0;
        hasCompletedTutorial = false;
        movingClouds = true;

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
