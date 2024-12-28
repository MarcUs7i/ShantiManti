using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlekseiHealthBar : MonoBehaviour
{
	private Aleksei _aleksei;
	private Animator _animator;
	private int _newHealth = 100;

	private void Start()
	{
		_aleksei = GetComponentInParent<Aleksei>();
		_animator = GetComponent<Animator>();
		_animator.SetBool("100", true);
	}

	private void Update()
    {
		_newHealth = (_aleksei.health * 100) / 200;

		int[] health = { 100, 75, 50, 25 };
		for (int i = 0; i < 4; i++)
		{
			_animator.SetBool(health[i].ToString(), health[i] == _newHealth);
		}
    }
}
