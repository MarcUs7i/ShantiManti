using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minusCounter : MonoBehaviour
{
    private static readonly int DisableAnimationID = Animator.StringToHash("Disable");
    private Text _counterText;
    public Animator animator;

    private void Awake()
    {
        _counterText = GetComponent<Text>();
    }

    private void Start()
    {
        if (!PlayerHealth.Minus)
        {
            Destroy(gameObject);
            return;
        }

        if (SC_2DCoin.TotalCoins <= 0)
        {
            PlayerHealth.Minus = false;
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
                if (SC_2DCoin.TotalCoins + levelRanges[i] < levelRanges[i])
                {
                    Destroy(gameObject);
                    PlayerHealth.Minus = false;
                    return;
                }

                _counterText.text = "-" + levelRanges[i].ToString();
                break;
            }
        }

        PlayerHealth.Minus = false;
        StartCoroutine(Animation());
    }


    private IEnumerator Animation()
    {
        yield return new WaitForSeconds(2.0f);
        animator.SetBool(DisableAnimationID, true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool(DisableAnimationID, false);
        yield return new WaitForSeconds(1.8f);
        Destroy(gameObject);
    }
}
