using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BirdBossHealthBar : MonoBehaviour
{
    BirdBoss birdBoss;
	Animator animator;

	void Start()
	{
		birdBoss = GetComponentInParent<BirdBoss>();
		animator = GetComponent<Animator>();
		animator.SetBool("100", true);
	}

	void Update()
    {
		int[] health = { 100, 75, 50, 25 };
		for (int i = 0; i < 4; i++)
		{
			animator.SetBool(health[i].ToString(), birdBoss.health >= health[i]);
		}
    }
}
