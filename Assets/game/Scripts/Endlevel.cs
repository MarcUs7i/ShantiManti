using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endlevel : MonoBehaviour
{
    public string levelToLoad = "Level2";
    public SceneFader sceneFader;
    public static int level = 0;
    private int SetLevel;

    void Awake()
    {
        //Make Collider2D as trigger 
        GetComponent<Collider2D>().isTrigger = true;
        level = PlayerSaving.Level;
        SetLevel = GetValueForSetLevel();
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.CompareTag("Player"))
        {
            GoToNextLevel();
        }
    }

    void Update()
    {
        if (level != PlayerSaving.Level)
        {
            if (PlayerSaving.Level < level)
            {
                PlayerSaving.Level = level;
                PlayerSaving.SavePlayer();
                //Debug.Log("Saved " + PlayerSaving.levels + " Level");
            }
        }
    }

    public void GoToNextLevel()
    {
        level = SetLevel;
        Debug.Log(level);
        sceneFader.FadeTo(levelToLoad);
    }

    public int GetValueForSetLevel()
    {
        if(!levelToLoad.StartsWith("level"))
        {
            return 0;
        }
        
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
