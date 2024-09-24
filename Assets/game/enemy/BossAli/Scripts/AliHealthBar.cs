using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AliHealthBar : MonoBehaviour
{
    public Ali ali;
	public Animator animator;
	int NewHealth = 100;
	int HealthCounter = 0;

	void Start()
	{
		animator.SetBool("100", true);
	}

	// Update is called once per frame
	void Update()
    {
		NewHealth = (ali.health * 100) / 200;
		//Debug.Log(NewHealth);

		if (NewHealth <= 100)
		{
			HealthCounter = 0;
		}
		if (NewHealth <= 75)
		{
			HealthCounter = 1;
		}
		if (NewHealth <= 50)
		{
			HealthCounter = 2;
		}
		if (NewHealth <= 25)
		{
			HealthCounter = 3;
		}

		if (HealthCounter == 0)
		{
			animator.SetBool("100", true);
			animator.SetBool("75", false);
			animator.SetBool("50", false);
			animator.SetBool("25", false);
		}
		if (HealthCounter == 1)
		{
			animator.SetBool("100", false);
			animator.SetBool("75", true);
			animator.SetBool("50", false);
			animator.SetBool("25", false);
		}
		if (HealthCounter == 2)
		{
			animator.SetBool("100", false);
			animator.SetBool("75", false);
			animator.SetBool("50", true);
			animator.SetBool("25", false);
		}
		if (HealthCounter == 3)
		{
			animator.SetBool("100", false);
			animator.SetBool("75", false);
			animator.SetBool("50", false);
			animator.SetBool("25", true);
		}
    }
}
