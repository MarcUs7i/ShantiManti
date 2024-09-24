using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class About : MonoBehaviour
{
    //public TMPro yourText;
    //public float speed = 25f;
    public bool move = false;
    float time = 0f;
    public float wait1 = 68.0f;
    public float wait2 = 85.0f;

    public SceneFader sceneFader;
    public string levelToLoad = "Start";

    public RectTransform obj;

    public float moveSpeed = 100.0f; // Adjust this to control the speed of movement
    public float movePercentage = 0.02f; // Adjust this to control the distance moved based on screen height


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UP());
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new Y position based on the screen height and movePercentage.
        float screenHeight = Screen.height;
        float newY = screenHeight * movePercentage;

        if (time == 2)
        {
            sceneFader.FadeTo(levelToLoad);
        }
        if (move == true)
        {
            // Calculate the new position based on time and moveSpeed
            Vector3 newPosition = transform.position + new Vector3(0, newY, 0) * moveSpeed * Time.deltaTime;

            // Update the UI element's position
            transform.position = newPosition;
        }
    }

    IEnumerator UP()
    {
        yield return new WaitForSeconds(5.0f);
        move = true;
        yield return new WaitForSeconds(wait1);
        move = false;
        obj.localPosition = new Vector3(0f,-500f,0f);
        time++;
        StartCoroutine(SecondUP());
    }
    IEnumerator SecondUP()
    {
        move = true;
        yield return new WaitForSeconds(wait2);
        move = false;
        obj.localPosition = new Vector3(0f,-500f,0f);
        time++;
        StartCoroutine(SecondUP());
    }

    /*public float moveSpeed = 100.0f; // Adjust this to control the speed of movement
    public float movePercentage = 0.02f; // Adjust this to control the distance moved based on screen height

    void Update()
    {
        // Calculate the new Y position based on the screen height and movePercentage.
        float screenHeight = Screen.height;
        float newY = screenHeight * movePercentage;

        // Calculate the new position based on time and moveSpeed
        Vector3 newPosition = transform.position + new Vector3(0, newY, 0) * moveSpeed * Time.deltaTime;

        // Update the UI element's position
        transform.position = newPosition;
    }*/
}
