using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AliHealthBar : MonoBehaviour
{
    public Ali ali;
	public Animator animator;
	int NewHealth = 100;

	void Start()
	{
		animator.SetBool("100", true);
	}

	void Update()
    {
		NewHealth = (ali.health * 100) / 200;

		int[] health = { 100, 75, 50, 25 };
		for (int i = 0; i < 4; i++)
		{
			animator.SetBool(health[i].ToString(), health[i] == NewHealth);
		}
    }
}
