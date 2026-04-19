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
	private LoadingScreen loadingScreenPrefab;

	[Header("Level Scenes")]
	[SerializeField]
	private string[] scenes;

	private string currentScene;
	private Coroutine loadSceneCoroutine;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
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
		LoadingScreen loadingScreen = Instantiate(loadingScreenPrefab);
		yield return loadingScreen.FadeIn();

		if (!string.IsNullOrEmpty(currentScene))
			SceneManager.UnloadSceneAsync(currentScene);

		AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
		while (!loadOperation.isDone)
		{
			yield return null;
		}
		currentScene = sceneName;

		yield return new WaitForEndOfFrame();
		yield return loadingScreen.FadeOut();
		Destroy(loadingScreen.gameObject);
		Time.timeScale = 1f;
	}
}
