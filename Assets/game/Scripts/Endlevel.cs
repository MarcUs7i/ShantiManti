using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endlevel : MonoBehaviour
{
    public string levelToLoad = "Level2";
    public SceneFader sceneFader;
    public static int Level;
    private int _setLevel;

    private void Awake()
    {
        //Make Collider2D as trigger 
        GetComponent<Collider2D>().isTrigger = true;
        Level = PlayerSaving.Level;
        _setLevel = GetValueForSetLevel();
    }

    private void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.CompareTag("Player"))
        {
            GoToNextLevel();
        }
    }

    private void Update()
    {
        if (Level != PlayerSaving.Level)
        {
            if (PlayerSaving.Level < Level)
            {
                PlayerSaving.Level = Level;
                PlayerSaving.SavePlayer();
            }
        }
    }

    public void GoToNextLevel()
    {
        Level = _setLevel;
        Debug.Log(Level);
        sceneFader.FadeTo(levelToLoad);
    }

    private int GetValueForSetLevel()
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
