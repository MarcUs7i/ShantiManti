using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    public Transform Hauer;
    public GameObject PauseButton;
    public GameObject LeftButton;
    public GameObject RightButton;
    public GameObject JumpButton;
    public GameObject CrouchButton;
    public GameObject FireButton;
    private Transform player;
    private Rigidbody2D rb;
    public float range = 15f;
    public float speed = 5f;

    public static bool BonusForJump = false;
    public Animator animator;
    public Animator GFX;
    bool walk = true;
    bool start = true;
    public float wait1 = 3f;
    public float wait2 = 1f;
    public float waitAfter = 5.0f;
    public Transform childTransform;
    public SceneFader sceneFader;
    public string levelToLoad = "About";

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // get the enemy's Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        BonusForJump = false;
        walk = false;
        start = true;
        GFX.SetBool("Stop", false);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < range)
        {
            Vector3 newPosition = new Vector3(12.60997f, -20.81544f, 0f);
            player.transform.position = newPosition;
            PauseButton.SetActive(false);
            LeftButton.SetActive(false);
            RightButton.SetActive(false);
            JumpButton.SetActive(false);
            CrouchButton.SetActive(false);
            FireButton.SetActive(false);
            BonusForJump = true;
            if (walk)
            {
                animator.SetBool("Walk", true);
            }
            if (!walk)
            {
                animator.SetBool("Walk", false);
            }
            if (start)
            {
                StartCoroutine(StartWalk());
            }
            if (Rettung.finished)
            {
                Rettung.finished = false;
                Destroy(childTransform.gameObject);
            }
            
        }
    }

    IEnumerator StartWalk()
    {
        GFX.SetBool("Stop", false);
        start = false;
        walk = true;
        yield return new WaitForSeconds(wait1);
        walk = false;
        GFX.SetBool("Stop", true);
        animator.SetBool("Fall", true);
        yield return new WaitForSeconds(wait2);
        animator.SetBool("Fall", false);
        animator.SetBool("After", true);
        Rettung.runScript = true;
        yield return new WaitForSeconds(waitAfter);
        BonusForJump = false;
        sceneFader.FadeTo(levelToLoad);
        MainMenu.ExitLevel = true;
        
    }
}
