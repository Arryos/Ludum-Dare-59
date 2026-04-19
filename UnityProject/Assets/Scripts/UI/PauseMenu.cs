using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] private GameObject objects;
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject controlsMenu;

	public void Start()
	{
		if (GameManager.Instance.IsPaused)
		{
			Open();
		}
		else
		{
			Close();
		}
	}

	public void Open()
	{
		OpenMainMenu();
		objects.SetActive(true);
	}

	public void Close()
	{
		objects.SetActive(false);
	}

	public void Resume()
	{
		GameManager.Instance.TogglePause();
	}

	public void OpenMainMenu()
	{
		mainMenu.SetActive(true);
		controlsMenu.SetActive(false);
	}

	public void OpenControls()
	{
		mainMenu.SetActive(false);
		controlsMenu.SetActive(true);
	}
}