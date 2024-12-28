using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BirdBossHealthBar : MonoBehaviour
{
	private BirdBoss _birdBoss;
	private Animator _animator;

	private void Start()
	{
		_birdBoss = GetComponentInParent<BirdBoss>();
		_animator = GetComponent<Animator>();
		_animator.SetBool("100", true);
	}

	private void Update()
    {
		int[] health = { 100, 75, 50, 25 };
		for (int i = 0; i < 4; i++)
		{
			_animator.SetBool(health[i].ToString(), _birdBoss.health >= health[i]);
		}
    }
}
