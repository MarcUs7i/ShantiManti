using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string levelToLoad = "MainLevel";
	public string start = "Start";
	public string levelselect = "LevelSelect";
	public string levelTutorial = "levelTutorial";
	public string level1 = "level1";
	public string level2 = "level2";
	public string level3 = "level3";
	public string level4 = "level4";
	public string level5 = "level5";
	public string level6 = "level6";
	public string level7 = "level7";
	public string level8 = "level8";
	public string level9 = "level9";
	public string level10 = "level10";
	public string level11 = "level11";
	public string level12 = "level12";
	public string level13 = "level13";
	public string level14 = "level14";
	public string level15 = "level15";
	public string level16 = "level16";
	public string level17 = "level17";
	public string level18 = "level18";
	public string level19 = "level19";
	public string level20 = "level20";
	public string bonus = "bonus";
	public string about = "About";

	public static int Tutorial;

	public SceneFader sceneFader;

	public static bool ExitLevel = false;

	void Awake()
	{
		PlayerSaving.LoadingPlayer = true;
		Tutorial = PlayerSaving.tutorial;
	}

	public void Play ()
	{
		sceneFader.FadeTo(levelToLoad);
	}

	public void StartScene ()
	{
		sceneFader.FadeTo(start);
		ExitLevel = true;
	}

	public void LevelSelect ()
	{
		sceneFader.FadeTo(levelselect);
		ExitLevel = true;
	}

	public void Level1 ()
	{
		if (Tutorial == 0)
		{
			sceneFader.FadeTo(levelTutorial);
			ExitLevel = false;
		}
		if (Tutorial == 1)
		{
			sceneFader.FadeTo(level1);
			ExitLevel = false;
		}
		
	}

	public void Level2 ()
	{
		sceneFader.FadeTo(level2);
		ExitLevel = false;
	}

	public void Level3 ()
	{
		sceneFader.FadeTo(level3);
		ExitLevel = false;
	}

	public void Level4 ()
	{
		sceneFader.FadeTo(level4);
		ExitLevel = false;
	}

	public void Level5 ()
	{
		sceneFader.FadeTo(level5);
		ExitLevel = false;
	}

	public void Level6 ()
	{
		sceneFader.FadeTo(level6);
		ExitLevel = false;
	}

	public void Level7 ()
	{
		sceneFader.FadeTo(level7);
		ExitLevel = false;
	}

	public void Level8 ()
	{
		sceneFader.FadeTo(level8);
		ExitLevel = false;
	}

	public void Level9 ()
	{
		sceneFader.FadeTo(level9);
		ExitLevel = false;
	}

	public void Level10 ()
	{
		sceneFader.FadeTo(level10);
		ExitLevel = false;
	}

	public void Level11 ()
	{
		sceneFader.FadeTo(level11);
		ExitLevel = false;
	}

	public void Level12 ()
	{
		sceneFader.FadeTo(level12);
		ExitLevel = false;
	}

	public void Level13 ()
	{
		sceneFader.FadeTo(level13);
		ExitLevel = false;
	}

	public void Level14 ()
	{
		sceneFader.FadeTo(level14);
		ExitLevel = false;
	}

	public void Level15 ()
	{
		sceneFader.FadeTo(level15);
		ExitLevel = false;
	}

	public void Level16 ()
	{
		sceneFader.FadeTo(level16);
		ExitLevel = false;
	}

	public void Level17 ()
	{
		sceneFader.FadeTo(level17);
		ExitLevel = false;
	}

	public void Level18 ()
	{
		sceneFader.FadeTo(level18);
		ExitLevel = false;
	}

	public void Level19 ()
	{
		sceneFader.FadeTo(level19);
		ExitLevel = false;
	}

	public void Level20 ()
	{
		sceneFader.FadeTo(level20);
		ExitLevel = false;
	}

	public void Bonus ()
	{
		sceneFader.FadeTo(bonus);
		ExitLevel = false;
	}

	public void About ()
	{
		sceneFader.FadeTo(about);
		ExitLevel = true;
	}

	public void Quit ()
	{// If Game is going public delete nr. 1 or set it as a comment
		Debug.Log("Exciting...");
		Application.Quit();
		#if UNITY_EDITOR 
			UnityEditor.EditorApplication.isPlaying = false; 
		#endif // 1 
	}

	public void Update (){
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
			#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
			#endif // 1
		}
	}

}