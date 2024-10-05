using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    private Transform Hauer;
    public GameObject[] disableObjects;
    private Transform player;
    private Animator animator;
    private Animator GFX;
    private Transform childTransform;
    private MainMenu mainMenu;
    private Ambulance ambulance;
    
    public static bool BonusForJump = false;

    [Header("Settings")]
    public float range = 15f;
    public float speed = 5f;

    [Header("Timings")]
    public float secondsToWalk = 3f;
    public float secondsToFall = 0.5f;
    public float timeout = 7.0f;

    bool walk = true;
    bool isCoroutineRunning = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Hauer = this.GetComponent<Transform>();
        childTransform = Hauer.GetChild(0);
        mainMenu = GameObject.FindObjectOfType<MainMenu>().GetComponent<MainMenu>();

        animator = GetComponent<Animator>();
        GFX = childTransform.GetComponent<Animator>();

        ambulance = GameObject.FindObjectOfType<Ambulance>().GetComponent<Ambulance>();

        BonusForJump = false;
        walk = false;
        GFX.SetBool("WalkAnimation", walk);
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < range)
        {
            Vector3 newPosition = new Vector3(12.60997f, -20.81544f, 0f);
            player.transform.position = newPosition;
            foreach (GameObject disableObject in disableObjects)
            {
                disableObject.SetActive(false);
            }
            BonusForJump = true;

            animator.SetBool("WalkTransform", walk);

            if (!isCoroutineRunning)
            {
                StartCoroutine(StartWalk());
            }
            if (ambulance.allowedToKillHauer)
            {
                ambulance.allowedToKillHauer = false;
                Destroy(childTransform.gameObject);
            }
            
        }
    }

    IEnumerator StartWalk()
    {
        isCoroutineRunning = true;
        walk = true;
        GFX.SetBool("WalkAnimation", walk);

        yield return new WaitForSeconds(secondsToWalk);

        walk = false;
        GFX.SetBool("WalkAnimation", walk);
        animator.SetBool("Fall", true);

        yield return new WaitForSeconds(secondsToFall);

        animator.SetBool("Fall", false);
        animator.SetBool("LayOnTheGround", true);
        StartCoroutine(ambulance.Script());

        yield return new WaitForSeconds(timeout);
        
        BonusForJump = false;
        mainMenu.AboutScene();
        
    }
}
