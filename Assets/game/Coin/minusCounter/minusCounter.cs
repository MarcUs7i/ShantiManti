using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minusCounter : MonoBehaviour
{
    Text counterText;
    public Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        counterText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Start()
    {
        if (PlayerHealth.minus == false)
        {
            Destroy(gameObject);
            //Debug.Log("false");
        }
        if (SC_2DCoin.totalCoins <= 0 && PlayerHealth.minus == true)
        {
            PlayerHealth.minus = false;
            Destroy(gameObject);
        }
        if (Endlevel.LevelForCoinBool <= 5 && PlayerHealth.minus == true)
		{
            if (SC_2DCoin.totalCoins < 5)
            {
                //counterText.text = "-" + SC_2DCoin.totalCoins + " ";
                Destroy(gameObject);
            }
            if (SC_2DCoin.totalCoins >= 5)
            {
                counterText.text = "-5 ";
            }
            PlayerHealth.minus = false;
            StartCoroutine(Animation());
		}
        if (Endlevel.LevelForCoinBool <= 9 && Endlevel.LevelForCoinBool >= 6 && PlayerHealth.minus == true)
		{
			if (SC_2DCoin.totalCoins < 10)
            {
                //counterText.text = "-" + SC_2DCoin.totalCoins;
                Destroy(gameObject);
            }
            if (SC_2DCoin.totalCoins >= 10)
            {
                counterText.text = "-10";
            }
            PlayerHealth.minus = false;
            StartCoroutine(Animation());
		}
		if (Endlevel.LevelForCoinBool <= 15 && Endlevel.LevelForCoinBool >= 10 && PlayerHealth.minus == true)
		{
			if (SC_2DCoin.totalCoins < 15)
            {
                //counterText.text = "-" + SC_2DCoin.totalCoins;
                Destroy(gameObject);
            }
            if (SC_2DCoin.totalCoins >= 15)
            {
                counterText.text = "-15";
            }
            PlayerHealth.minus = false;
            StartCoroutine(Animation());
		}
		if (Endlevel.LevelForCoinBool <= 20 && Endlevel.LevelForCoinBool >= 16 && PlayerHealth.minus == true)
		{
			if (SC_2DCoin.totalCoins < 20)
            {
                //counterText.text = "-" + SC_2DCoin.totalCoins;
                Destroy(gameObject);
            }
            if (SC_2DCoin.totalCoins >= 20)
            {
                counterText.text = "-20";
            }
            PlayerHealth.minus = false;
            StartCoroutine(Animation());
		}

        /*if (PlayerHealth.minus == true)
        {
            StartCoroutine(Animation());
            PlayerHealth.minus = false;
        }*/
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
