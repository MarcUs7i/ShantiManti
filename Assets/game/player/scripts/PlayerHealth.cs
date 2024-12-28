using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

	public int health = 100;
	private bool _coinHealth;
	public static bool Minus;

	public void TakeDamage(int damage)
	{
		if (MainMenu.ExitLevel)
		{
			return;
		}
		health -= damage;

		StartCoroutine(DamageAnimation());

		if (health <= 0)
		{
			Die();
		}
	}


	private void Update() {
		/*if (Input.GetKeyDown(KeyCode.B))
		{
			TakeDamage(20);
		}*/

		if(Enemy.TookDamage && !Pause.IsPause)
		{
			TakeDamage(20);
			Enemy.TookDamage = false;
		}
		
		if (SC_2DCoin.TotalCoins % 20 == 0 && SC_2DCoin.TotalCoins > 0)
		{
			health = 100;
			_coinHealth = !_coinHealth;
		}
	}


	public void Die()
	{
		int[] coinDeductions = { 5, 10, 15, 20 };
		for (int i = 0; i < coinDeductions.Length; i++)
		{
			if (SC_2DCoin.TotalCoins > coinDeductions[i] && health <= 0)
			{
				// Subtract coins based on the level
				if (PlayerSaving.Level <= coinDeductions[i])
				{
					SC_2DCoin.TotalCoins -= coinDeductions[i];
					Minus = true;
				}

				// Ensure the totalCoins does not go below 0
				if (SC_2DCoin.TotalCoins < 0)
				{
					SC_2DCoin.TotalCoins = 0;
				}

				// Save the updated coin count
				PlayerSaving.Coins = SC_2DCoin.TotalCoins;
				PlayerSaving.SavePlayer();
			}
		}
		SoundBar.SceneReloaded = true;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	IEnumerator DamageAnimation()
	{
		SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

		for (int i = 0; i < 3; i++)
		{
			foreach (SpriteRenderer sr in srs)
			{
				Color c = sr.color;
				c.a = 0;
				sr.color = c;
			}

			yield return new WaitForSeconds(.1f);

			foreach (SpriteRenderer sr in srs)
			{
				Color c = sr.color;
				c.a = 1;
				sr.color = c;
			}

			yield return new WaitForSeconds(.1f);
		}
	}

}
