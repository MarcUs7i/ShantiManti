using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endlevel : MonoBehaviour
{
    public string levelToLoad = "Level2";
    public SceneFader sceneFader;
    public static int level = 0;
    private int SetLevel;
    public static bool nextLevel = false;

    void Awake()
    {
        //Make Collider2D as trigger 
        GetComponent<Collider2D>().isTrigger = true;
        level = PlayerSaving.level;
        //PlayerSaving.LoadPlayer();
        Debug.Log(level);
        SetLevel = GetValueForSetLevel();
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
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
        if (level != PlayerSaving.level && !PlayerSaving.Deleteing)
        {
            if (PlayerSaving.level < level)
            {
                PlayerSaving.level = level;
                PlayerSaving.SavePlayer();
                //Debug.Log("Saved " + PlayerSaving.levels + " Level");
            }
        }

        if (nextLevel == true)
        {
            nextLevel = false;
            level = SetLevel;
            Debug.Log(level);
            sceneFader.FadeTo(levelToLoad);
        }
    }

    public int GetValueForSetLevel()
    {
        string levelInString = "";
        for(int i = 0; i < levelToLoad.Length; i++)
        {
            if (char.IsDigit(levelToLoad[i]))
            {
                levelInString += levelToLoad[i];
            }
        }
        return int.Parse(levelInString);
    }
}
