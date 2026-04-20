using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
	[field: SerializeField] public PlayerDamagable PlayerDamagable { get; private set; }

	[Header("Pause")]
	[SerializeField] private InputAction pauseInput;

	[SerializeField] private PauseMenu pauseMenu;
	[SerializeField] private GameObject uiCamera;

	public Action<bool> onPauseToggled;

	public Frequencies PlayerFrequency => PlayerDamagable.Frequency;
	public bool IsPaused { get; private set; }


	public void Awake()
	{
		pauseInput.performed += ctx => TogglePause();
		pauseInput.Enable();
		if (pauseMenu)
		{
			pauseMenu.Close();
		}
	}

	public void TogglePause(bool openPauseMenu = true)
	{
		IsPaused = !IsPaused;

		onPauseToggled?.Invoke(IsPaused);

		Time.timeScale = IsPaused ? 0f : 1f;
		if (openPauseMenu
		    && pauseMenu)
		{
			if (IsPaused)
			{
				uiCamera.SetActive(true);
				pauseMenu.Open();
			}
			else
			{
				uiCamera.SetActive(false);
				pauseMenu.Close();
			}
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
		SceneFlowManager.Instance.LoadNextLevel();
	}
}