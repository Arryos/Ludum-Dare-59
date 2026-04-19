using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
	[field: SerializeField]
	public PlayerDamagable PlayerDamagable { get; private set; }

	[Header("Pause")]
	[SerializeField]
	private InputAction pauseInput;
	[SerializeField]
	PauseMenu pauseMenu;

	public Frequencies PlayerFrequency => PlayerDamagable.Frequency;
	public Action<bool> onPauseToggled;

	private bool isPaused;


	public void Awake()
	{
		pauseInput.performed += (ctx) => TogglePause();
		pauseInput.Enable();
		if (pauseMenu)
		{
			pauseMenu.gameObject.SetActive(false);
		}
	}

	public void TogglePause()
	{
		isPaused = !isPaused;

		onPauseToggled?.Invoke(isPaused);

		Time.timeScale = isPaused ? 0f : 1f;
		if (pauseMenu)
		{
			pauseMenu.gameObject.SetActive(isPaused);
		}
	}

	[ContextMenu("Game Over")]
	public void GameOver()
	{
		SceneFlowManager.Instance.ResetScene();
	}

	[ContextMenu("Level Complete")]
	public void LevelComplete()
	{
		SceneFlowManager.Instance.ResetScene();
	}
}
