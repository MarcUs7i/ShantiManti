using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minusCounter : MonoBehaviour
{
    Text counterText;
    public Animator animator;

    void Awake()
    {
        counterText = GetComponent<Text>();
    }

    void Start()
    {
        if (!PlayerHealth.minus)
        {
            Destroy(gameObject);
            return;
        }

        if (SC_2DCoin.totalCoins <= 0)
        {
            PlayerHealth.minus = false;
            Destroy(gameObject);
            return;
        }

        // Define the level ranges for each coin deduction
        int[] levelRanges = { 5, 10, 15, 20 };

        // Loop through the level ranges
        for (int i = 0; i < levelRanges.Length; i++)
        {
            // Check if player's level is within the range for the current coin value
            if (PlayerSaving.level <= levelRanges[i] && (i == 0 || PlayerSaving.level > levelRanges[i - 1]))
            {
                if (SC_2DCoin.totalCoins + levelRanges[i] < levelRanges[i])
                {
                    Destroy(gameObject);
                    PlayerHealth.minus = false;
                    return;
                }

                counterText.text = "-" + levelRanges[i].ToString();
                break;
            }
        }

        PlayerHealth.minus = false;
        StartCoroutine(Animation());
    }


    IEnumerator Animation()
    {
        yield return new WaitForSeconds(2.0f);
        animator.SetBool("Disable", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Disable", false);
        yield return new WaitForSeconds(1.8f);
        Destroy(gameObject);
    }
}
