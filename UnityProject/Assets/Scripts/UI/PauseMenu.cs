using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	public void Resume()
	{
		GameManager.Instance.TogglePause();
	}

	public void Quit()
	{

	}
}
