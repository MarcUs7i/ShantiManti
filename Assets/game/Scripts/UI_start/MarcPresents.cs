using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcPresents : MonoBehaviour
{
    public SceneFader sceneFader;
    public string levelToLoad = "Start";
    public float howMuchToWait = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartScene());
    }

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(howMuchToWait);
        sceneFader.FadeTo(levelToLoad);
    }

}
