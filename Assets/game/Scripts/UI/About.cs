using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class About : MonoBehaviour
{
    public bool move;
    private float _time;
    public float wait1 = 46.0f;
    public float wait2 = 55.0f;

    public SceneFader sceneFader;
    public string levelToLoad = "Start";

    public RectTransform obj;

    public float moveSpeed = 5.0f; // Adjust this to control the speed of movement
    public float movePercentage = 0.02f; // Adjust this to control the distance moved based on screen height


    private void Start()
    {
        StartCoroutine(Up());
    }

    private void Update()
    {
        // Calculate the new Y position based on the screen height and movePercentage.
        float screenHeight = Screen.height;
        float newY = screenHeight * movePercentage;

        if (Mathf.Approximately(_time, 2))
        {
            sceneFader.FadeTo(levelToLoad);
        }
        if (move)
        {
            // Calculate the new position based on time and moveSpeed
            Vector3 newPosition = transform.position + new Vector3(0, newY, 0) * (moveSpeed * Time.deltaTime);

            // Update the UI element's position
            transform.position = newPosition;
        }
    }

    private IEnumerator Up()
    {
        yield return new WaitForSeconds(5.0f);
        move = true;
        yield return new WaitForSeconds(wait1);
        move = false;
        obj.localPosition = new Vector3(0f,-500f,0f);
        _time++;
        StartCoroutine(SecondUp());
    }

    private IEnumerator SecondUp()
    {
        move = true;
        yield return new WaitForSeconds(wait2);
        move = false;
        obj.localPosition = new Vector3(0f,-500f,0f);
        _time++;
        StartCoroutine(SecondUp());
    }
}
