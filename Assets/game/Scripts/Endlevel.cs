using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endlevel : MonoBehaviour
{
    public string levelToLoad = "Level2";
    public SceneFader sceneFader;
    public static int level = 0;
    public int SetLevel = 1;
    public static bool nextLevel = false;
    public static int LevelForCoinBool = 0;

    void Awake()
    {
        //Make Collider2D as trigger 
        GetComponent<Collider2D>().isTrigger = true;
        level = PlayerSaving.levels;
        PlayerSaving.LoadingPlayer = true;
        Debug.Log(level);
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        //Destroy the coin if Object tagged Player comes in contact with it
        if (c2d.CompareTag("Player"))
        {
            level = SetLevel;
            Debug.Log(level);
            sceneFader.FadeTo(levelToLoad);
        }
    }

    void Update()
    {
        //Debug.Log(level);
        if (level != PlayerSaving.levels && PlayerSaving.Deleteing == false)
        {
            if (PlayerSaving.levels < level)
            {
                PlayerSaving.levels = level;
                PlayerSaving.SavingPlayer = true;
                //Debug.Log("Saved " + PlayerSaving.levels + " Level");
            }
        }

        // this one is not just for coin bool used. It's used in Pause.cs too!
        LevelForCoinBool = SetLevel;

        if (nextLevel == true)
        {
            nextLevel = false;
            level = SetLevel;
            Debug.Log(level);
            sceneFader.FadeTo(levelToLoad);
        }
    }
}
