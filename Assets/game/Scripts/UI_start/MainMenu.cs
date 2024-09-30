using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	private string[] scenes = {"MarcPresents", "Start", "LevelSelect", "levelTutorial", "level1", "level2", "level3", "level4", "level5", "level6", "level7", "level8", "level9", "level10", "level11", "level12", "level13", "level14", "level15", "level16", "level17", "level18", "level19", "level20", "bonus", "About"};

	public GameObject[] standardUI;
	public GameObject[] secondUI;
	public GameObject[] changeWhenEnabledUI;
	public GameObject[] changeWhenDisabledUI;
	public static bool tutorial;

	public SceneFader sceneFader;

	public static bool ExitLevel = false;

	void Awake()
	{
		//PlayerSaving.LoadPlayer();
		tutorial = PlayerSaving.hasCompletedTutorial;
	}

	public void StartScene()
	{
		sceneFader.FadeTo(scenes[1]);
		ExitLevel = true;
	}

	public void LevelSelect()
	{
		sceneFader.FadeTo(scenes[2]);
		ExitLevel = true;
	}

	public void StartLevel(int level)
	{
		tutorial = PlayerSaving.hasCompletedTutorial;
		if (level == 1 && !tutorial)
		{
			sceneFader.FadeTo(scenes[3]);
			ExitLevel = false;
			return;
		}
		level += 3;
		sceneFader.FadeTo(scenes[level]);
		ExitLevel = false;
	}

	public void About()
	{
		sceneFader.FadeTo(scenes[^1]);
		ExitLevel = true;
	}

	public void Quit()
	{
		Debug.Log("Exciting...");
		Application.Quit();
		#if UNITY_EDITOR 
			UnityEditor.EditorApplication.isPlaying = false; 
		#endif
	}

	public void ToggleUI()
	{
		//The check
		foreach (GameObject ui in changeWhenEnabledUI)
		{
			if (!ui.activeSelf) return;
		}
		foreach (GameObject ui in changeWhenDisabledUI)
		{
			if (ui.activeSelf) return;
		}

		//The change
		foreach (GameObject ui in standardUI)
		{
			ui.SetActive(!ui.activeSelf);
		}
		foreach (GameObject ui in secondUI)
		{
			ui.SetActive(!ui.activeSelf);
		}
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
		{
			string currentScene = SceneManager.GetActiveScene().name;
			if (currentScene == "LevelSelect" || currentScene == "levelTutorial" || currentScene == "bonus" || currentScene == "About")
			{
				StartScene();
			}
			else if (currentScene == "MarcPresents")
			{
				Quit();
			}
			else
			{
				ToggleUI();
			}

			//Pause in game is handled in Pause.cs
		}
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && SceneManager.GetActiveScene().name == "Start")
		{
			LevelSelect();
		}
	}

}