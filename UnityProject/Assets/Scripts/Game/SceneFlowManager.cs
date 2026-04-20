using DG.Tweening;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowManager : Singleton<SceneFlowManager>
{
	[SerializeField]
	private int currentSceneIndex;

	[SerializeField]
	private LoadingScreen loadingScreen;

	[Header("Level Scenes")]
	[SerializeField]
	private string[] scenes;

	private string currentScene;
	private Coroutine loadSceneCoroutine;

	private void Awake()
	{
		if(SceneFlowManager.Instance != this)
		{
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(gameObject);
		LoadScene(scenes[0]);
	}

	public void ResetScene()
	{
		if(currentScene == null)
		{
			currentScene = SceneManager.GetActiveScene().name;
		}
		LoadScene(currentScene);
	}

	public void LoadNextLevel()
	{
		if (currentSceneIndex >= scenes.Length - 1) return;

		currentSceneIndex++;
		LoadScene(scenes[currentSceneIndex]);
	}

	public void LoadScene(string sceneName)
	{
		DOTween.KillAll();
		Time.timeScale = 1f;
		if (loadSceneCoroutine != null) StopCoroutine(loadSceneCoroutine);

		loadSceneCoroutine = StartCoroutine(LoadSceneCoroutine(sceneName));
	}

	private IEnumerator LoadSceneCoroutine(string sceneName)
	{
		yield return loadingScreen.FadeIn();

		if (!string.IsNullOrEmpty(currentScene))
			SceneManager.UnloadSceneAsync(currentScene);

		AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
		while (!loadOperation.isDone)
		{
			yield return null;
		}

		currentScene = sceneName;
		SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));

		yield return new WaitForEndOfFrame();
		yield return loadingScreen.FadeOut();
		Time.timeScale = 1f;
	}
}
