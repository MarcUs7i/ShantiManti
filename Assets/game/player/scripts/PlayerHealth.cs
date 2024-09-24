using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

	public int health = 100;

	public GameObject deathEffect;
	bool CoinHealth = false;
	public static bool minus;

	public void TakeDamage(int damage)
	{
		if (MainMenu.ExitLevel == false)
		{
			health -= damage;

			StartCoroutine(DamageAnimation());

			if (health <= 0)
			{
				Die();
			}
		}
	}


	void Update() {
		/*if (Input.GetKeyDown(KeyCode.B))
		{
			TakeDamage(20);
		}*/
		
		if (DiePlayer.KillPlayer == true)
		{
			DiePlayer.KillPlayer = false;
			Die();
		}


		if(Enemy.TakedDamage == true && Pause.IsPause == false)
		{
			TakeDamage(20);
			Enemy.TakedDamage = false;
		}
		if (SC_2DCoin.totalCoins == 20 && CoinHealth == false)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 40 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 60 && CoinHealth == false)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 80 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 100 && CoinHealth == false)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 120 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 140 && CoinHealth == false)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 160 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 180 && CoinHealth == false)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 200 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 220 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 240 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 260 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 280 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 300 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 320 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 340 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 360 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 380 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 400 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 420 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 440 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 460 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 480 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 500 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 520 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 540 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 560 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 580 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 600 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 620 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 640 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 660 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 680 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 700 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 720 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 740 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 760 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 780 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 800 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 820 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 840 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 860 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 880 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 900 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 920 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 940 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 960 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
		if (SC_2DCoin.totalCoins == 980 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = true;
		}
		if (SC_2DCoin.totalCoins == 1000 && CoinHealth == true)
		{
			health = 100;
			CoinHealth = false;
		}
	}


	void Die()
	{
		if (SC_2DCoin.totalCoins > 0 && health <= 0)
        {
            // Subtract coins based on the level
            if (Endlevel.LevelForCoinBool <= 5)
            {
                SC_2DCoin.totalCoins -= 5;
                minus = true;
            }
            else if (Endlevel.LevelForCoinBool <= 9 && Endlevel.LevelForCoinBool >= 6)
            {
                SC_2DCoin.totalCoins -= 10;
                minus = true;
            }
            else if (Endlevel.LevelForCoinBool <= 15 && Endlevel.LevelForCoinBool >= 10)
            {
                SC_2DCoin.totalCoins -= 15;
                minus = true;
            }
            else if (Endlevel.LevelForCoinBool <= 20 && Endlevel.LevelForCoinBool >= 16)
            {
                SC_2DCoin.totalCoins -= 20;
                minus = true;
            }

            // Ensure the totalCoins does not go below 0
            if (SC_2DCoin.totalCoins < 0)
            {
                SC_2DCoin.totalCoins = 0;
            }

            // Save the updated coin count
            PlayerSaving.savedCoins = SC_2DCoin.totalCoins;
            PlayerSaving.SavingPlayer = true;
            Debug.Log("Saved " + PlayerSaving.savedCoins);
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
