using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	private readonly string[] _scenes = {"MarcPresents", "Start", "LevelSelect", "levelTutorial", "level1", "level2", "level3", "level4", "level5", "level6", "level7", "level8", "level9", "level10", "level11", "level12", "level13", "level14", "level15", "level16", "level17", "level18", "level19", "level20", "bonus", "About"};

	[Header("UIs")]
	public GameObject[] standardUI;
	public GameObject[] secondUI;
	[Header("The UI changes only if the following are enabled/disabled")]
	public GameObject[] changeOnlyWhenEnabledUI;
	public GameObject[] changeOnlyWhenDisabledUI;

	public static bool ExitLevel;
	private static InputActions _inputActions;
	private SceneFader _sceneFader;

	void Start()
	{
		_sceneFader = FindFirstObjectByType<SceneFader>().GetComponent<SceneFader>();
	}
	
	private void Awake()
	{
		_inputActions = new InputActions();
		_inputActions.Player.Start.performed += _ =>
		{
			if(SceneManager.GetActiveScene().name == "Start")
			{
				LevelSelect();
			}
		};
		_inputActions.Player.Pause.performed += _ =>
		{
			var currentScene = SceneManager.GetActiveScene().name;
			switch (currentScene)
			{
				case "LevelSelect":
				case "levelTutorial":
				case "bonus":
				case "About":
					StartScene();
					break;
				default:
					ToggleUI();
					break;
			}

			//Pause in game is handled in Pause.cs
		};
	}
	
	private void OnEnable() => _inputActions.Enable();
	private void OnDisable() => _inputActions.Disable();

	public void StartScene() => LoadScene(1);
	public void LevelSelect() => LoadScene(2);
	public void AboutScene() => LoadScene(_scenes.Length - 1);

	public void StartLevel(int level)
	{
		var tutorial = PlayerSaving.HasCompletedTutorial;
		if (level == 1 && !tutorial)
		{
			_sceneFader.FadeTo(_scenes[3]);
			ExitLevel = false;
			return;
		}
		level += 3;
		_sceneFader.FadeTo(_scenes[level]);
		ExitLevel = false;
	}

	public void LoadScene(int scene)
	{
		_sceneFader.FadeTo(_scenes[scene]);
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

	private void ToggleUI()
	{
		//The check
		foreach (var ui in changeOnlyWhenEnabledUI)
		{
			if (!ui.activeSelf) return;
		}
		foreach (var ui in changeOnlyWhenDisabledUI)
		{
			if (ui.activeSelf) return;
		}

		//The change
		foreach (var ui in standardUI)
		{
			ui.SetActive(!ui.activeSelf);
		}
		foreach (var ui in secondUI)
		{
			ui.SetActive(!ui.activeSelf);
		}
	}
}