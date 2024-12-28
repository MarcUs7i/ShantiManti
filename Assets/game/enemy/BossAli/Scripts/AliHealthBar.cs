using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AliHealthBar : MonoBehaviour
{
	private Ali _ali;
	private Animator _animator;
	private int _newHealth = 100;

	private void Start()
	{
		_ali = GetComponentInParent<Ali>();
		_animator = GetComponent<Animator>();
		_animator.SetBool("100", true);
	}

	private void Update()
    {
		_newHealth = (_ali.health * 100) / 200;

		int[] health = { 100, 75, 50, 25 };
		for (int i = 0; i < 4; i++)
		{
			_animator.SetBool(health[i].ToString(), health[i] == _newHealth);
		}
    }
}
