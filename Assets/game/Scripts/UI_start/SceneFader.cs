using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour {

	public Image img;
	public AnimationCurve curve;

	private void Start ()
	{
		StartCoroutine(FadeIn());
	}

	public void FadeTo(string scene)
	{
		StartCoroutine(FadeOut(scene));
	}

	private IEnumerator FadeIn ()
	{
		var t = 1f;

		while (t > 0f)
		{
			t -= Time.deltaTime;
			var a = curve.Evaluate(t);
			img.color = new Color (0f, 0f, 0f, a);
			yield return 0;
		}
	}

	private IEnumerator FadeOut(string scene)
	{
		var t = 0f;

		while (t < 1f)
		{
			t += Time.deltaTime;
			var a = curve.Evaluate(t);
			img.color = new Color(0f, 0f, 0f, a);
			yield return 0;
		}

		SceneManager.LoadScene(scene);
	}

}
